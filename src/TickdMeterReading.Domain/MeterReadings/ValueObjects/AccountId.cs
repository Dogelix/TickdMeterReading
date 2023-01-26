using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.ValueObjects
{
    public class MeterReadingId
    {
        private readonly Guid _id;

        public MeterReadingId(Guid id)
        {
            if (id.Equals(Guid.Empty))
                throw new ArgumentNullException($"Meter Reading Id does not have any value");

            _id = id;
        }

        public Guid Value => _id;

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
