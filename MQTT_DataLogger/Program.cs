using MQTT_DataLogger;
using System;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

var factory = new DataLoggerContextFactory();
var context = factory.CreateDbContext();

Subscribe();


void Subscribe()
{
    var url = "localhost";
    var topic = "home/#";
    // create client instance
    MqttClient client = new MqttClient(url);

    // register to message received 
    client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

    Console.WriteLine($"DataLogger is listening on {url} to the topic {topic}");

    string clientId = Guid.NewGuid().ToString();
    client.Connect(clientId);

    // subscribe to the topic "/home/temperature"
    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
}

void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
{
    var msg = new Message();
    msg.Msg = Encoding.UTF8.GetString(e.Message);
    
    if (!(msg.Msg == "stopped" || msg.Msg == "started"))
    {
        msg.Date = DateTime.Now;
        msg.Topic = e.Topic;
        // handle message received
        if (msg.Topic == "home/time")
        {
            var date = new DateTime(Convert.ToInt64(msg.Msg));
            Console.WriteLine($"Received = {date} on topic {msg.Topic}");
        }
        else
        {
            Console.WriteLine($"Received = {msg.Msg} on topic {msg.Topic}");
        }

        var count = context.Messages.Where(m => m.Topic == msg.Topic).Count();
        var lastMessage = context.Messages.OrderBy(m => m.Date).Where(m => m.Topic == msg.Topic).LastOrDefault();
        if (lastMessage != null /*&& lastMessage.Msg != msg.Msg*/)
        {
            if (count >= 10 && msg.Topic == "home/temperature" || msg.Topic == "home/light")
            {
                context.Messages.Remove(context.Messages.Where(m => m.Topic == msg.Topic).FirstOrDefault());
            }
            
            context.Messages.Add(msg);
            context.SaveChanges();
            Console.WriteLine("Saved a new Message");
        }
        else if (lastMessage == null)
        {
            context.Messages.Add(msg);
            context.SaveChanges();
            Console.WriteLine("Saved first Message in this topic");
        }
    }
}
