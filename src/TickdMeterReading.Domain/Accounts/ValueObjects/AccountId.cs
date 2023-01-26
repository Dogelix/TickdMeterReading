using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.ValueObjects
{
    public class AccountId
    {
        private readonly int _accountId;

        public AccountId(int accountId)
        {
            if (accountId.Equals(Guid.Empty))
                throw new ArgumentNullException($"Account Id does not have any value");

            _accountId = accountId;
        }

        public int Value => _accountId;

        public override string ToString()
        {
            return _accountId.ToString();
        }
    }
}
