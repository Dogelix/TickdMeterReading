using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Application.Exceptions
{
    public class DataNotFoundRepositoryException : Exception
    {
        public DataNotFoundRepositoryException(string message) : base("Data Not Found: " + message) { }
    }
}
