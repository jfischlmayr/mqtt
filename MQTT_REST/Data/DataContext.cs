using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_REST
{
    public class DataContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
