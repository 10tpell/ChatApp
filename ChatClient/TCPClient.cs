using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace ChatClient
{
    class TCPClient
    {
        TcpClient client;
        IPEndPoint serverEndPoint;
        NetworkStream clientStream;
        private ChatUtils.Console console;
        System.Timers.Timer timer;
 
        public TCPClient(string p1, int p2, ChatUtils.Console console)
        {
            client = new TcpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(p1), p2);
            client.Connect(serverEndPoint);
            clientStream = client.GetStream();
            timer = new System.Timers.Timer();
            timer.Interval = 100;
            timer.Elapsed += new ElapsedEventHandler(timerElapsed);
            timer.Start();
            this.console = console;
        }

        private void timerElapsed(object sender, EventArgs e)
        {
            checkForMessages();
        }

        public void SendMessage(string Message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(Message);

            this.clientStream.Write(buffer, 0, buffer.Length);
            this.clientStream.Flush();
        }

        private void checkForMessages()
        {
            byte[] message = new byte[4096];
            int bytesRead;

            bytesRead = 0;

            try
            {
                //waits for a message
                bytesRead = clientStream.Read(message, 0, 4096);
            }
            catch (Exception e)
            {
                //error occured
                console.writeLine(e.ToString());
            }

            if (bytesRead == 0)
            {
                //client has left server
                return;
            }

            //message has been recieved
            ASCIIEncoding encoder = new ASCIIEncoding();
            string msg = encoder.GetString(message, 0, bytesRead);
            console.writeLine("DEBUG: Server Sent: " + msg);

            if (msg.TrimStart('/').ToUpper() == "QUIT")
            {
                console.writeLine("Server Disconnected, Quiting...");
                client.Close();
                this.clientStream.Close();
                timer.Stop();
            }

        }
        
    }
}
