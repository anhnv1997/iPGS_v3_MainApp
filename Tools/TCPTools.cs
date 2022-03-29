using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace iParking.Tools
{
    public class TCPTools
    {
        public static string STX = "02";
        public static string ETX = "03";
        public static bool Start_TCP_Server(string ipAddress, int Port, ref Socket TCPServer, ref IPEndPoint ipEpBroadcast)
        {
            try
            {
                ipEpBroadcast = new IPEndPoint(IPAddress.Parse(ipAddress), Port);
                TCPServer = new Socket(SocketType.Stream, ProtocolType.Tcp);
                TCPServer.Connect(ipEpBroadcast);
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
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(ip_Address, 500))
                {
                    if (Start_TCP_Server(ip_Address, port, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.ASCII.GetBytes(command);

                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            TCPServer.Close();
                            TCPServer = null;
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
        public static string ExecuteCommand_Ascii(string ip_Address, int port, string command, int delayTime)
        {
            string ret = "";
            try
            {
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(ip_Address, 500))
                {
                    if (Start_TCP_Server(ip_Address, port, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.ASCII.GetBytes(command);

                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);                               
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            TCPServer.Close();
                            TCPServer = null;
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
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (Start_TCP_Server(comport, baudrate, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.UTF8.GetBytes(command);

                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            TCPServer.Close();
                            TCPServer = null;
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
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (Start_TCP_Server(comport, baudrate, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.ASCII.GetBytes(command);
                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
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
                            TCPServer.Close();
                            TCPServer = null;
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
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (Start_TCP_Server(comport, baudrate, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = System.Text.Encoding.UTF8.GetBytes(command);
                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
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
                            TCPServer.Close();
                            TCPServer = null;
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
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (Start_TCP_Server(comport, baudrate, ref TCPServer, ref ipEpBroadcast))
                    {
                        //command = "SetRelay?/Relay=01/State=OFF";
                        byte[] bData = encodingType.GetBytes(command);
                        // Send data
                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
                                byte checksum = 1;
                                for (int i = 1; i < readLen - 2; i++)
                                    checksum += bRebuff[i];

                                message = ByteUI.Get_Message(bRebuff, readLen, ref viewraw);

                                ByteUI.Get_Message(bData, bData.Length, ref viewraw);
                                ret = System.Text.Encoding.UTF8.GetString(bRebuff, 0, readLen);
                            }
                        }
                        catch (Exception ex)
                        {
                            ret = ex.ToString();
                        }
                        finally
                        {
                            TCPServer.Close();
                            TCPServer = null;
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
        public static string ExecuteCommand(string comport, int baudrate,string command, ref string viewraw, ref string[] message, int delayTime, string startStr, Encoding encodingType)
        {
            string ret = "";
            try
            {
                viewraw = "";
                message = null;
                Socket TCPServer = null;
                IPEndPoint ipEpBroadcast = null;

                if (NetWorkTools.IsPingSuccess(comport, 500))
                {
                    if (Start_TCP_Server(comport, baudrate, ref TCPServer, ref ipEpBroadcast))
                    {
                        byte[] bData = encodingType.GetBytes(command);
                        TCPServer.Send(bData);
                        TCPServer.Shutdown(SocketShutdown.Send);
                        TCPServer.ReceiveTimeout = 2000;
                        Thread.Sleep(150);
                        viewraw = "";
                        message = null;
                        try
                        {
                            if (TCPServer.ReceiveBufferSize != 0)
                            {
                                byte[] bRebuff = new byte[TCPServer.ReceiveBufferSize];
                                int readLen = TCPServer.Receive(bRebuff);
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
                            TCPServer.Close();
                            TCPServer = null;
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
