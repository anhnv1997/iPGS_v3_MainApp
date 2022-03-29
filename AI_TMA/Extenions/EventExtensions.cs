using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kztek.AI.Extenions
{
    public static class EventExtensions
    {
        public static T GetData<T> (this uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var rawData = Encoding.UTF8.GetString(e.Message);
            var result = JsonConvert.DeserializeObject<T>(rawData);

            return result;
        }
    }
}
