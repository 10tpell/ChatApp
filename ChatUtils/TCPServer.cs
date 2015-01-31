using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatUtils
{
    public class TCPServer
    {
        private TcpListener tcpListener;
        public bool loop = true;
        public bool on = true;
        private Thread listenThread;
        private ChatUtils.Console console;
        private CommandHandler cmdHandler;
        public List<NetworkStream> allConnections;
  
        public TCPServer(ChatUtils.Console console)
        {
            this.console = console;
            try
            {
                this.tcpListener = new TcpListener(IPAddress.Any, 3000);
                this.listenThread = new Thread(new ThreadStart(ListenForClients));
                this.listenThread.Start();
                allConnections = new List<NetworkStream>();
            }
            catch(Exception e)
            {
                console.writeLine(e.ToString());
            }
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();
            while(loop)
            {
                //Waits for a client
                TcpClient client = this.tcpListener.AcceptTcpClient();
              
                //creating a thread for coms with client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
            tcpListener.Stop();
        }

        public void sendMessage(NetworkStream clientStream, string message)
        {
            ASCIIEncoding encoder1 = new ASCIIEncoding();
            byte[] buffer = encoder1.GetBytes(message);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        public CommandHandler getCmdHandler()
        {
            return cmdHandler;
        }

        public void broadcastMessage(string message)
        {
            ASCIIEncoding encoder1 = new ASCIIEncoding();
            byte[] buffer = encoder1.GetBytes(message);

            for (int i = 0; i < allConnections.Count; i++)
            {
                allConnections[i].Write(buffer, 0, buffer.Length);
                allConnections[i].Flush();
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            allConnections.Add(clientStream);

            byte[] message = new byte[4096];
            int bytesRead;

            string IP = ((Socket)clientStream.GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(clientStream, null)).RemoteEndPoint.ToString();

            sendMessage(clientStream, "/Connected");

            cmdHandler = new CommandHandler(console, this, clientStream);

            while (loop)
            {
                bytesRead = 0;

                try
                {
                    //waits for a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch(Exception e)
                {
                    //error occured
                    console.writeLine(e.ToString());
                }

                if (bytesRead == 0)
                {
                    //client has left server
                    console.writeLine(IP + " Has left the server");
                    break;
                }

                //message has been recieved
                ASCIIEncoding encoder = new ASCIIEncoding();
                string msg = encoder.GetString(message, 0, bytesRead);
                if (msg.Contains("/"))
                {
                    cmdHandler.cmd(msg);
                }
                else
                {
                    console.writeLine(IP + ": " + encoder.GetString(message, 0, bytesRead));
                    broadcastMessage(IP + ": " + msg);
                }
            }

            //close client
            tcpClient.Close();
            clientStream.Close();    
        }
    }
}
