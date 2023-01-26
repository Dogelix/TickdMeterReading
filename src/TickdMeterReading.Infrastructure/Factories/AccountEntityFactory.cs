using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Infrastructure.Factories
{
    public class AccountEntityFactory : IAccountFactory
    {
        public Account Create(AccountId accountId, FirstName firstName, LastName lastName) 
        {
            return new AccountFactory(accountId, firstName, lastName);
        }
    }
}
