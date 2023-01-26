using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.ValueObjects
{
    public class LastName
    {
        private string _name;

        public LastName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", "Last Name has no value.");

            _name = name;
        }

        public string Value => _name;


    }
}
