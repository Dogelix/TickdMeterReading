using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Infrastructure.Factories
{
    public class MeterReadingEntityFactory : IMeterReadingFactory
    {
        public MeterReading Create(AccountId accountId, MeterReadingDateTime dateTime, MeterReadValue value)
        {
            return new MeterReadingFactory(accountId, dateTime, value);
        }
    }
}
