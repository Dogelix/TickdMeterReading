using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.MeterReadings.ValueObjects
{
    public class MeterReadingDateTime
    {
        private DateTime _dateTime;

        public MeterReadingDateTime(DateTime dateTime) 
        {

            _dateTime = dateTime;
        }

        public DateTime Value => _dateTime;

        public override string ToString()
        {
            return _dateTime.ToString("YYYY-MM-DD hh:mm:ss");
        }
    }
}
