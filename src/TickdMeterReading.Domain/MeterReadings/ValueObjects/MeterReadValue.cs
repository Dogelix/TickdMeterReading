using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.MeterReadings.ValueObjects
{
    public class MeterReadValue
    {
        private string _value;

        public MeterReadValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("vlaue", "Value has no value.");

            _value = value;
        }

        public string Value => _value;
    }
}
