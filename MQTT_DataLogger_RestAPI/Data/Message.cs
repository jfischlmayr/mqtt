using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_DataLogger_RestAPI
{
    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
    }
}
