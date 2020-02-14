using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.ViewModel
{
    public class AlarmClearCount
    {
        /// <summary>
        /// 已解除警报
        /// </summary>
        public int ClearNum { get; set; }
        /// <summary>
        /// 未解除警报
        /// </summary>
        public int UnClearNum { get; set; }
    }
}
