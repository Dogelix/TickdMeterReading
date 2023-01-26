using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Domain.Accounts.ValueObjects
{
    public class FirstName
    {
        private string _name;

        public FirstName(string name) 
        { 
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("name", "First Name has no value.");
            
            _name = name;
        }

        public string Value => _name;
    }
}
