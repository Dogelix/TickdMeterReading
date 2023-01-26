using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Infrastructure.Factories
{
    public class AccountFactory : Account
    {
        public AccountFactory() { }

        public AccountFactory(AccountId accountId, FirstName firstName, LastName lastName)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
