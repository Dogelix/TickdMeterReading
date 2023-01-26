using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Domain.Accounts
{
    public interface IAccountFactory
    {
        Account Create(AccountId accountId, FirstName firstName, LastName lastName);
    }
}
