using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.MeterReadings;

namespace TickdMeterReading.Infrastructure.Repositories
{
    public class MeterReadingRespository : IMeterReadingRepository
    {
        private readonly IMeterReadingFactory _factory;

        public MeterReadingRespository(IMeterReadingFactory meterReadingFactory) 
        { 
            _factory = meterReadingFactory;
        }

        public Task<MeterReading> Add(MeterReading entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<MeterReading>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<MeterReading> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
