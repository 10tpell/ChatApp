using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatServer
{
    class TCPServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private ChatUtils.Console console;
        
        public TCPServer(ChatUtils.Console console)
        {
            this.console = console;
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while(true)
            {
                //Waits for a client
                TcpClient client = this.tcpListener.AcceptTcpClient();
              
                //creating a thread for coms with client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            string IP = ((Socket)clientStream.GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(clientStream, null)).RemoteEndPoint.ToString();

            ASCIIEncoding encoder1 = new ASCIIEncoding();
            byte[] buffer = encoder1.GetBytes("/Connected");

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            while (true)
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

                }

                if (bytesRead == 0)
                {
                    //client has left server
                    break;
                }

                //message has been recieved
                ASCIIEncoding encoder = new ASCIIEncoding();
                console.writeLine(IP + ": " + encoder.GetString(message, 0, bytesRead));
            }

            //close client
            tcpClient.Close();
        }
    }
}
