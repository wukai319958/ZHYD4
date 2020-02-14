using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 点位信息（可以作为容器使用）
    /// </summary>
    public class Location : SeedWork.Entity
    {
        protected Location()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Location(int useMapType, double latitude, double longitude, string province, string city, string district, string street, string streetNumber, bool enabled, string address)
            : this()
        {
            UseMapType = useMapType;
            Latitude = latitude;
            Longitude = longitude;
            Province = province;
            City = city;
            District = district;
            Street = street;
            StreetNumber = streetNumber;
            Enabled = enabled;
            Address = address;
        }

        /// <summary>
        /// 默认为百度地图 0
        /// </summary>
        public int UseMapType { get; private set; }
        /// <summary>
        /// 纬度（南纬为负数）
        /// </summary>
        public double Latitude { get; private set; }
        /// <summary>
        /// 经度（西经为负数）
        /// </summary>
        public double Longitude { get; private set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        [StringLength(500)]
        public string Address { get; private set; }
        /// <summary>
        /// 省
        /// </summary>
        [StringLength(450)]
        public string Province { get; private set; }
        /// <summary>
        /// 市
        /// </summary>
        [StringLength(450)]
        public string City { get; private set; }
        /// <summary>
        /// 区
        /// </summary>
        [StringLength(450)]
        public string District { get; private set; }
        /// <summary>
        /// 街道
        /// </summary>
        [StringLength(450)]
        public string Street { get; private set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        [StringLength(450)]
        public string StreetNumber { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }

        public void SetLatitude(double latitude)
        {
            Latitude = latitude;
        }

        public void SetLongitude(double longitude)
        {
            Longitude = longitude;
        }
        /// <summary>
        /// 行政区域设置
        /// </summary>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        public void Regionalism(string province, string city, string district)
        {
            Province = province;
            City = city;
            District = district;
        }
    }

}
