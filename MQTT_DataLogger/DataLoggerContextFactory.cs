using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_DataLogger
{
    class DataLoggerContextFactory : IDesignTimeDbContextFactory<DataLoggerContext>
    {
        public DataLoggerContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string mySqlConnectionStr = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DataLoggerContext>();
            optionsBuilder

                .UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));

            return new DataLoggerContext(optionsBuilder.Options);
        }
    }
}
