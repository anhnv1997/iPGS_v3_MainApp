using iParking;
using Kztek.AI;
using Kztek.AI.Extenions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AI_TMA
{
    public class ParkingLot
    {
        private MQTTLib _mqtt;
        private ParkingLotTopics _topics;

        public event OnDataReceive OnDataReceived;

        public ParkingLot(string mqtt_ip, string mqtt_user, string mqtt_password, string deviceToken = "default_token")
        {
            _mqtt = new MQTTLib(mqtt_ip, mqtt_user, mqtt_password);
        }
        public void Start()
        {
            int code = _mqtt.Connect();
            LogHelper.Logger_Info("connect "+code);
            _mqtt.ParkingLotRecResponseReceived += _mqtt_ParkingLotRecResponseReceived;
        }
        private void _mqtt_ParkingLotRecResponseReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.Message);
            var topic = e.Topic;
            LogHelper.Logger_Info("Excecute data " + topic);
            try
            {
                if (topic == "scp/device/default_token/Parkinglot")
                {
                    LogHelper.Logger_Info("Start Get Data " + topic);

                    ParkingLotRecResponseEventArg _ParkingLotRecResponseEventArg = new ParkingLotRecResponseEventArg(e);
                    OnDataReceived?.Invoke(this, _ParkingLotRecResponseEventArg);
                    LogHelper.Logger_Info("Active Event " + data);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger_Error(ex.Message);
            }
            
        }
    }
    public delegate void ParkingLotRecResponseReceivedEvent(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e);
    public delegate void OnDataReceive(object sender, ParkingLotRecResponseEventArg e);


    public class ParkingLotRecResponseEventArg : EventArgs
    {
        public ParkingLotRec Data { get; }

        public uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs BaseEvent { get; }

        public ParkingLotRecResponseEventArg(uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e) : base()
        {
            BaseEvent = e;
            Data = e.GetData<ParkingLotRec>();
        }
    }
}
