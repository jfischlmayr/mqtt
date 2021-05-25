using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_DataLogger
{
    class DataLoggerContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DataLoggerContext(DbContextOptions<DataLoggerContext> options) : base(options) { }
    }
}
