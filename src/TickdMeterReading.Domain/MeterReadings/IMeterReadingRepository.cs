using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Domain.MeterReadings
{
    public interface IMeterReadingRepository : IRepository<MeterReading>
    {
    }
}
