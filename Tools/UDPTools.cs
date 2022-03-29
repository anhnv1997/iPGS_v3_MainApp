using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace iParking.Tools
{
    public class UDPTools
    {
        private const string STX = "02";
        private const string ETX = "03";
        public static bool Start_UDP_Server(string ipAddress, int Port, ref Socket UdpServer, ref IPEndPoint ipEpBroadcast)
        {
            try
            {
                UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                ipEpBroadcast = new IPEndPoint(IPAddress.Parse(ipAddress), Port);
                UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                return true;
            }
            catch
            { }
            return false;
        }

        public static string ExecuteCommand_Ascii(string ip_Address, int port, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(ip_Address, 500))
                {
                    if (UDPTools.Start_UDP_Server(ip_Address, port, ref UdpServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.ASCII.GetBytes(command);

                        // Send data
                        UdpServer.SendTo(bData, ipEpBroadcast);

                        UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch(Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }                        
                    }
                    else
                    {
                        ret = "Socket Start Error";
                    }
                    Thread.Sleep(delayTime);
                }

                else
                {
                    ret = "Ping Error";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
        public static string ExecuteCommand_UTF8(string comport, int baudrate, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (UDPTools.Start_UDP_Server(comport, baudrate, ref UdpServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.UTF8.GetBytes(command);

                        UdpServer.SendTo(bData, ipEpBroadcast);

                        UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch(Exception ex)
                        {
                            ret = ex.ToString();                            
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }
                    }
                    else
                    {
                        ret = "Socket Start Error";
                    }
                    Thread.Sleep(delayTime);
                }
                else
                {
                    ret = "Ping Error";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
        
        public static string ExecuteCommand_Ascii(string comport, int baudrate, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime, string startStr)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (UDPTools.Start_UDP_Server(comport, baudrate, ref UdpServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.ASCII.GetBytes(command);
                        UdpServer.SendTo(bData, ipEpBroadcast);
                        UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);
                                if (isSuccess(message[0], startStr))// && checksum == bRebuff[readLen - 2])
                                {
                                    ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                    string s1 = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                                    ret = System.Text.Encoding.UTF8.GetString(bRebuff, 1, readLen - 2);                                    
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }
                    }

                }
                else
                    Thread.Sleep(delayTime);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
        public static string ExecuteCommand_UTF8(string comport, int baudrate, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime, string startStr)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (UDPTools.Start_UDP_Server(comport, baudrate, ref UdpServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.UTF8.GetBytes(command);
                        UdpServer.ReceiveTimeout = 2000;
                        UdpServer.SendTo(bData, ipEpBroadcast);

                        //UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);
                                if (isSuccess(message[0], startStr))// && checksum == bRebuff[readLen - 2])
                                {
                                    ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                    string s1 = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                                    ret  = System.Text.Encoding.UTF8.GetString(bRebuff, 1, readLen - 2);                                    
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }
                        return ret;
                    }
                }
                else
                    Thread.Sleep(delayTime);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
        
        public static string ExecuteCommand(string comport, int baudrate, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime, Encoding encodingType)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (UDPTools.Start_UDP_Server(comport, baudrate, ref UdpServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = encodingType.GetBytes(command);
                        // Send data
                        UdpServer.SendTo(bData, ipEpBroadcast);
                        // Update timeout
                        UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch(Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }
                    }
                }
                else
                    Thread.Sleep(delayTime);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
        public static string ExecuteCommand(string comport, int baudrate, byte[] buffer, string command, ref string viewraw, ref string[] message, int delayTime, string startStr, Encoding encodingType)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket UdpServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (UDPTools.Start_UDP_Server(comport, baudrate, ref UdpServer, ref ipEpBroadcast))
                    {
                        byte[] bData = encodingType.GetBytes(command);
                        UdpServer.SendTo(bData, ipEpBroadcast);
                        UdpServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (UdpServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[UdpServer.ReceiveBufferSize];
                                int readLen = UdpServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);
                                if (isSuccess(message[0], startStr))// && checksum == bRebuff[readLen - 2])
                                {
                                    ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                    string s1 = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                                    ret = System.Text.Encoding.UTF8.GetString(bRebuff, 1, readLen - 2);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            UdpServer.Close();
                            UdpServer = null;
                            ipEpBroadcast = null;
                        }
                    }
                }
                else
                    Thread.Sleep(delayTime);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
               
        private static bool isSuccess(string response, string searchStr)
        {
            if (response.Contains(searchStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
