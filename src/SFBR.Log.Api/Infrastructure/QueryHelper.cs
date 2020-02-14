using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SFBR.Log.Api.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueryHelper
    {
        #region Core
        /// <summary>
        /// 动态获取查询条件（sql脚本），无法做类型检查，映射的实体对象可以起别名。需要注意sql注入的问题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll">请求的键值对集合</param>
        /// <param name="prex">键值对key值前缀</param>
        /// <param name="placeholder">数据库占位符</param>
        /// <param name="Quoted">关键字转义</param>
        /// <returns></returns>
        public static Tuple<string, Dictionary<string, object>> GetWhereToParString(this IEnumerable<KeyValuePair<string, StringValues>> coll, string prex = "query", string placeholder = "@", string Quoted = "\"")
        {
            if (coll == null)
            {
                throw new ArgumentException("coll");
            }
            else if (coll.Count() == 0)
            {
                return new Tuple<string, Dictionary<string, object>>(null, new Dictionary<string, object>());
            }
          
            var result1 = coll.GroupBy(t => t.Key);

            var key2 = result1.Where(x => x.Key.Trim().StartsWith("query"));
            var keys= key2.Select(s => s.Key);

            if (keys.Count() == 0)
            {
                return new Tuple<string, Dictionary<string, object>>(null, new Dictionary<string, object>());
            }
            StringBuilder result = new StringBuilder();
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            int start = -1;
            int last = -1;
            int index = 0;
            foreach (string key in keys)
            {
                start = key.IndexOf('_');
                last = key.LastIndexOf('_');
                //属性对应的值
                string[] values = coll.FirstOrDefault(where=>where.Key == key).Value.ToArray();
                if (start < 0 || last < 0 || values.Length <= 0 || (values.Length == 1 && string.IsNullOrEmpty(values[0]))) continue;
                //列名
                string column = GetOriginalName(key, start, last);
                if (string.IsNullOrEmpty(column)) continue;
                //获取比较符
                string comparison = key.Substring(last + 1);
                if (result.Length > 0)
                {
                    result.Append(" and " + CreateSqltext(column, comparison, values, paramList, index, placeholder, Quoted));
                }
                else
                {
                    result.Append(CreateSqltext(column, comparison, values, paramList, index, placeholder, Quoted));
                }
                index++;
            }
            return new Tuple<string, Dictionary<string, object>>(result.ToString(), paramList);
        }
        
        const string p_fix = "p_";
        private static string CreateSqltext(string column, string comparison, string[] values, Dictionary<string, object> paramList, int index, string placeholder = "@", string Quoted = "\"")
        {
            if (paramList == null)
            {
                throw new Exception("解析查询条件发生异常！");
            }
            string[] arr = comparison.Split('$');
            string parName = placeholder + p_fix + index.ToString() + "_" + column;//参数名
            string quotedName = Quoted + column + Quoted;
            switch (arr[0])
            {
                case "=":
                case "eq":
                case "equal":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " = " + parName;
                case ">":
                case "gt":
                case "greaterthan":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " > " + parName;
                case ">=":
                case "gte":
                case "greaterthanorequal":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " >= " + parName;
                case "<":
                case "lt":
                case "lessthan":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " < " + parName;
                case "<=":
                case "lte":
                case "lessthanorequal":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " <= " + parName;
                case "!=":
                case "<>":
                case "ne":
                case "notequal":
                    paramList.Add(parName, ConverData(values[0], comparison));
                    return quotedName + " <> " + parName;
                case "like"://注意数据类型，如果不是字符串的必须转换为字符串
                    paramList.Add(parName, string.Concat("%", values[0], "%"));
                    return quotedName + " like " + parName;
                case "nlike"://注意数据类型，如果不是字符串的必须转换为字符串
                case "notlike"://注意数据类型，如果不是字符串的必须转换为字符串
                    paramList.Add(parName, string.Concat("%", values[0], "%"));
                    return quotedName + " not like " + parName;
                case "sw":
                case "startwith":
                    paramList.Add(parName, string.Concat("%", values[0]));
                    return quotedName + " like " + parName;
                case "nsw":
                case "nstartwith":
                    paramList.Add(parName, string.Concat("%", values[0]));
                    return quotedName + " not like " + parName;
                case "ew":
                case "endwith":
                    paramList.Add(parName, string.Concat(values[0], "%"));
                    return quotedName + " like " + parName;
                case "new":
                case "nendwith":
                    paramList.Add(parName, string.Concat(values[0], "%"));
                    return quotedName + " not like " + parName;
                case "in":
                    List<string> inList = new List<string>();
                    for (int idx = 0; idx < values.Length; idx++)
                    {
                        string name = parName + idx.ToString();
                        paramList.Add(name, ConverData(values[idx], comparison));
                        inList.Add(name);
                    }
                    return quotedName + " in (" + string.Join(",", inList) + ")";
                case "nin":
                case "notin":
                    List<string> nInList = new List<string>();
                    for (int idx = 0; idx < values.Length; idx++)
                    {
                        string name = parName + idx.ToString();
                        paramList.Add(name, ConverData(values[idx], comparison));
                        nInList.Add(name);
                    }
                    return quotedName + " not in (" + string.Join(",", nInList) + ")";
                default://默认操作符为等于比较符
                    return quotedName + " = " + parName; ;
            }
        }

        private static object ConverData(string val, string comparison)
        {
            var arr = comparison.Split('$');
            if (arr.Length <= 1)//未指定类型
            {
                DateTime date;
                if (DateTime.TryParse(val, out date))
                {
                    return CheckAndFixDate(date, comparison);
                }
                else
                {
                    return val;
                }
            }
            else
            {
                switch (arr[1].ToLower())
                {
                    case "num":
                        return long.Parse(val);
                    case "flt":
                        return double.Parse(val);
                    case "y":
                    case "d":
                    case "h":
                    case "m":
                    case "s":
                    case "f":
                        return CheckAndFixDate(DateTime.Parse(val), comparison);
                    default:
                        DateTime date;
                        if (DateTime.TryParse(val, out date))
                        {
                            return CheckAndFixDate(date, comparison);
                        }
                        else
                        {
                            return val;
                        }
                }
            }
        }
        private static string GetOriginalName(string key, int start, int last)
        {
            string name = "";
            //只有一个下滑杠只取前一部分
            if (start == last)
            {
                name = key.Substring(0, last - 1).Replace("￥", "_");
            }
            else
            {
                name = key.Substring(start + 1, last - start - 1).Replace("￥", "_");//只取中间部分
            }
            return name;
        }


        private static object CheckAndFixDate(object value, string comparison)
        {
            if (value is DateTime)
            {
                string[] arr = comparison.Split('$');
                switch (arr[0].ToLower())
                {
                    case ">":
                    case "gt":
                    case "greaterthan":
                    case ">=":
                    case "gte":
                    case "greaterthanorequal":
                        return DateGreaterFix((DateTime)value, arr);
                    case "<":
                    case "lt":
                    case "lessthan":
                    case "<=":
                    case "lte":
                    case "lessthanorequal":
                        return DateLessFix((DateTime)value, arr);
                }
            }
            return value;
        }

        /// <summary>
        /// 修复时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        private static object DateLessFix(DateTime value, string[] formatter)
        {
            if (formatter.Length == 2)
            {
                switch (formatter[1])
                {
                    case "y"://精确到年
                    case "Y":
                        value = new DateTime(value.Year + 1, 1, 1);
                        break;
                    case "M":
                        value = value.AddMonths(1);
                        value = new DateTime(value.Year, value.Month, 1);
                        break;
                    case "d":
                    case "D":
                        value = value.AddDays(1);
                        value = new DateTime(value.Year, value.Month, value.Day);
                        break;
                    case "h":
                    case "H":
                        value = value.AddHours(1);
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);
                        break;
                    case "m":
                        value = value.AddMinutes(1);
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                        break;
                    case "s":
                    case "S":
                        value = value.AddSeconds(1);
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
                        break;
                    case "f"://精确到毫秒
                    case "F":
                        value = value.AddMilliseconds(1);
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
                        break;
                    default://默认到天
                        value = value.AddDays(1);
                        value = new DateTime(value.Year, value.Month, value.Day);
                        break;
                }
            }
            else
            {
                value = value.Date.AddDays(1);
            }
            return value;
        }


        private static object DateGreaterFix(DateTime value, string[] formatter)
        {
            if (formatter.Length == 2)
            {
                switch (formatter[1])
                {
                    case "y"://精确到年
                    case "Y":
                        value = new DateTime(value.Year, 1, 1);
                        break;
                    case "M":
                        value = new DateTime(value.Year, value.Month, 1);
                        break;
                    case "d":
                    case "D":
                        value = new DateTime(value.Year, value.Month, value.Day);
                        break;
                    case "h":
                    case "H":
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);
                        break;
                    case "m":
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                        break;
                    case "s":
                    case "S":
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
                        break;
                    case "f"://精确到毫秒
                    case "F":
                        value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
                        break;
                    default://默认到天
                        value = value.Date;
                        break;
                }
            }
            else
            {
                value = value.Date;
            }
            return value;
        }


        private static readonly NameValueCollection emptyColl = new NameValueCollection();
        /// <summary>
        /// 空集合
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static NameValueCollection Empty(this NameValueCollection coll)
        {
            return emptyColl;
        }

        #endregion
    }

}
