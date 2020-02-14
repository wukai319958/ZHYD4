using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class Moment:SeedWork.ValueObject
    {
        private Moment()
        {
        }
        public Moment(int month,int day,int hour,int minute,int second)
        {
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
        }
        public int Month { get;private set; }

        public int Day { get;private set; }

        public int Hour { get;private set; }

        public int Minute { get;private set; }

        public int Second { get;private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Month;
            yield return Day;
            yield return Hour;
            yield return Minute;
            yield return Second;
        }
    }
}
