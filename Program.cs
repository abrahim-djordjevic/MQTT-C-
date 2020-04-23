using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT
{
    class Program
    {
        static void Main(string[] args)
        {
            //connect to broker
            MqttClient client = new MqttClient("broker.hivemq.com");
            string Id = Guid.NewGuid().ToString();
            client.Connect(Id);

            //subscribe to a list of topics
            client.Subscribe(new string[] { "/test" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += client_MqttMsgPublishRecieved;
        }


        static void MqttSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Debug.WriteLine("Subscribed for ID: " + e.MessageId);
        }

        static void client_MqttMsgPublishRecieved(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine("Recieved " + Encoding.UTF8.GetString(e.Message) + " on Topic:" + e.Topic);
        }
    }
}
