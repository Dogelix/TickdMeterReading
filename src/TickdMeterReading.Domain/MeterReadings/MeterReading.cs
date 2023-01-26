using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.Domain.MeterReadings
{
    public class MeterReading : IAggregateRoot
    {
        public MeterReadingId Id { get; set; }
        public AccountId AccountId { get; set; }
        public MeterReadingDateTime MeterReadingDateTime { get; set; }
        public MeterReadValue MeterReadValue { get; set; }
    }
}
