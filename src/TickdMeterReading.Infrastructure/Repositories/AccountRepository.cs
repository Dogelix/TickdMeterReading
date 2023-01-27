using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;

namespace TickdMeterReading.Infrastructure.Repositories
{
    public class AccountRepository : IAccountsRepository
    {
        private readonly TickdTechTestDb _tickdTechTestDb;
        private readonly IAccountFactory _factory;

        public AccountRepository(TickdTechTestDb tickdTechTestDb, IAccountFactory factory) 
        {
            _tickdTechTestDb = tickdTechTestDb;
            _factory = factory;
        }

        public async Task<Account> Add(Account entity)
        {
            try
            {
                using (var cmd = _tickdTechTestDb.Connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO `Accounts` VALUES (@AccountId, @FirstName, @LastName);";
                    BindAddAccountParams(entity, cmd);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return entity;
        }

        public async Task<List<Account>> FindAll()
        {
            List<Account> returnValue;
            try
            {
                using (var cmd = _tickdTechTestDb.Connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM `Accounts`;";
                    returnValue = await ReadAllAsync(await cmd.ExecuteReaderAsync());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return (returnValue.Count == 0) ? null: returnValue;
        }

        public async Task<Account> FindById(int id)
        {
            List<Account> returnValue;
            try
            {
                using (var cmd = _tickdTechTestDb.Connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM `Accounts` WHERE AccountId = @AccountId;";
                    BindIdParams(id, cmd);
                    returnValue = await ReadAllAsync(await cmd.ExecuteReaderAsync());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return returnValue.FirstOrDefault();
        }

        public async Task Remove(int id)
        {
            try
            {
                using (var cmd = _tickdTechTestDb.Connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE * FROM `Accounts` WHERE AccountId = @AccountId;";
                    BindIdParams(id, cmd);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        private async Task<List<Account>> ReadAllAsync(DbDataReader reader)
        {
            var accounts = new List<Account>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var account = _factory.Create(
                            new AccountId(reader.GetInt16(0)),
                            new FirstName(reader.GetString(1)),
                            new LastName(reader.GetString(2)));
                }
            }

            return accounts;
        }

        private void BindAddAccountParams(Account entity, MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@AccountId",
                DbType = DbType.String,
                Value = entity.AccountId.Value,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@FirstName",
                DbType = DbType.String,
                Value = entity.FirstName.Value,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@LastName",
                DbType = DbType.String,
                Value = entity.LastName.Value,
            });
        }

        private void BindIdParams(int id, MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter { ParameterName = "@AccountId", DbType = DbType.Int32, Value = id });
        }
    }
}
