using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickdMeterReading.Infrastructure
{
    public class TickdTechTestDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public TickdTechTestDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
