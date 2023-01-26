using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickdMeterReading.Domain.Accounts.ValueObjects;

namespace TickdMeterReading.Application.ViewModels
{
    public class AccountViewModel
    {
        public AccountViewModel() { }

        public AccountViewModel(AccountId accountId, FirstName firstName, LastName lastName)
        {
            AccountId = accountId.ToString();
            FirstName = firstName.Value;
            LastName = lastName.Value;
        }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
