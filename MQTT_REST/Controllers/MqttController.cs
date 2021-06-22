using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTT_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly DataContext _context;

        public MqttController(DataContext context)
        {
            _context = context;
        }

        [Route("temp")]
        [HttpGet]
        public List<Message> GetTemperatures()
        {
            var result = _context.Messages.Where(m => m.Topic == "home/temperature").ToList();
            return result;
        }

        [Route("light")]
        [HttpGet]
        public Message GetLightState()
        {
            return _context.Messages.Where(m => m.Topic == "home/light").FirstOrDefault();
        }
    }
}
