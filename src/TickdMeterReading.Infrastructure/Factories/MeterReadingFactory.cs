using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Infrastructure.Factories
{
    public class MeterReadingFactory : MeterReading
    {
        public MeterReadingFactory() { }

        public MeterReadingFactory( AccountId accountId, MeterReadingDateTime dt, MeterReadValue val)
        {
            Id = (new MeterReadingId(Guid.NewGuid()));
            AccountId = accountId;
            MeterReadingDateTime = dt;
            MeterReadValue = val;
        }
    }
}
