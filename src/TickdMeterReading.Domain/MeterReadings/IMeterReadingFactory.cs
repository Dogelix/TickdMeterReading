using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Domain.MeterReadings
{
    public interface IMeterReadingFactory
    {
        MeterReading Create(AccountId accountId, MeterReadingDateTime dateTime, MeterReadValue value);
    }
}
