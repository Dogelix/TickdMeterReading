using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Infrastructure.Repositories
{
    public class AccountRepository : IAccountsRepository
    {
        public AccountRepository() 
        {

        }

        public Task<Account> Add(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> FindAll()
        {
            throw new NotImplementedException();
        }
        public Task<Account> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
