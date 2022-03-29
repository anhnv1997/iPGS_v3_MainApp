using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParking
{
    public class TMA_Server
    {
        private string id;
        private string name;
        private string code;
        private string description;
        private string ip;
        private int port;
        private string username;
        private string password;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
        public string Description { get => description; set => description = value; }
        public string Ip { get => ip; set => ip = value; }
        public int Port { get => port; set => port = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
