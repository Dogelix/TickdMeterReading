using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;

namespace TickdMeterReading.Infrastructure.Repositories
{
    public class MeterReadingRespository : IMeterReadingRepository
    {
        private readonly TickdTechTestDb _tickdTechTestDb;

        public MeterReadingRespository(TickdTechTestDb tickdTechTestDb, IMeterReadingFactory meterReadingFactory) 
        {
            _tickdTechTestDb = tickdTechTestDb;
        }

        public async Task<MeterReading> Add(MeterReading entity)
        {
            try
            {
                using (var cmd = _tickdTechTestDb.Connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO `meter_reading` VALUES (@Id, @AccountId, @MeterReadingDateTime, @MeterReadValue);";
                    BindParams(entity, cmd);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return entity;
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

        private void BindParams(MeterReading entity, MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.String,
                Value = entity.Id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@AccountId",
                DbType = DbType.Int16,
                Value = entity.AccountId,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@MeterReadingDateTime",
                DbType = DbType.DateTime,
                Value = entity.MeterReadingDateTime,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@MeterReadValue",
                DbType = DbType.String,
                Value = entity.MeterReadValue,
            });
        }
    }
}
