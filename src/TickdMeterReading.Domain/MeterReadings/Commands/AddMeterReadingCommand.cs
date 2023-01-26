using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.MeterReadings.Commands
{
    public class AddMeterReadingCommand : MeterReadingCommand
    {
        public AddMeterReadingCommand(string id, int accountId, DateTime meterReadingDateTime, string meterReadValue) 
        {
            Id = id;
            AccountId = accountId;
            MeterReadValue = meterReadValue;
            MeterReadingDateTime = meterReadingDateTime;
        }
    }
}
