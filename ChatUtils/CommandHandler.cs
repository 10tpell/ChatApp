using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatUtils
{
    public class CommandHandler
    {
        Console console;
        TCPServer server;
        private NetworkStream clientStream;
        public bool on = true; //for closing the app

        public CommandHandler(Console con, TCPServer server, NetworkStream clientStream)
        {
            this.clientStream = clientStream;
            this.console = con;
            this.server = server;
        }

        public CommandHandler(Console con, TCPServer server)
        {
            this.console = con;
            this.server = server;
        }

        public void cmd(string command)
        {
            string cmd = command.TrimStart('/');
            try
            {
                console.writeLine("User used:" + cmd + " Command");
            }
            catch
            {

            }

            if (cmd.ToUpper() == "STOP")
            {
                server.broadcastMessage("/Quit");
                server.loop = false;
                on = false;
            }
        }
    }
}
