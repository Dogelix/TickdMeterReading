using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.Commands
{
    public class CreateNewAccountCommand : AccountCommand
    {
        public CreateNewAccountCommand(int id, string firstName, string lastName) 
        {
            AccountId = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
