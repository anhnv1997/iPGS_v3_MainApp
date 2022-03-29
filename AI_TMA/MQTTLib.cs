using iParking;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
namespace AI_TMA
{
    public class MQTTLib
    {
        public event ParkingLotRecResponseReceivedEvent ParkingLotRecResponseReceived;
        private MqttClient mqttClient = null;
        public string TcpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public MqttClient _MqttClient { get => mqttClient; set => mqttClient = value; }

        public MQTTLib(string tcpServer, string username, string password)
        {
            this.TcpServer = tcpServer;
            this.Username = username;
            this.Password = password;
        }

        public int Connect()
        {
            _MqttClient = new MqttClient(this.TcpServer, 1883, false, MqttSslProtocols.None, null, null);
            _MqttClient.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            _MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            _MqttClient.Subscribe(new string[] { "#" }, new byte[MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE]);
            byte code = _MqttClient.Connect(Guid.NewGuid().ToString(), this.Username, this.Password);
            return code;
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            LogHelper.Logger_Info("GetData from mqtt " + Encoding.UTF8.GetString(e.Message));
            ParkingLotRecResponseReceived?.Invoke(this, e);
        }
    }
}
