using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Domain.Accounts
{
    public class Account : IAggregateRoot
    {
        public AccountId AccountId { get; set; }
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
    }
}
