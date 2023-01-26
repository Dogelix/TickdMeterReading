using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.Events
{
    public class AccountCreatedEvent : AccountEvent
    {
        public AccountCreatedEvent(int id, string firstName, string lastName)
        {
            AccountId = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
