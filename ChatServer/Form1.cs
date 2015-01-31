using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

using ChatUtils;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        ChatUtils.Console console;
        TCPServer server;
        CommandHandler cmdHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            console = new ChatUtils.Console();
            server = new TCPServer(console);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = console.getText();
            try
            {
                if (server.getCmdHandler().on == false)
                {
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                // code for debugging console.writeLine(ex.ToString());
            }

            try
            {
                if (cmdHandler.on == false)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                // code for debugging console.writeLine(ex.ToString());
            }
            //populates listbox with ip addresses of connected clients
            try
            {
                for (int i = 0; i < server.allConnections.Count; i++)
                {
                    if (listBox1.Items.Contains(((Socket)server.allConnections[i].GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(server.allConnections[i], null)).RemoteEndPoint.ToString()) != true)
                    {
                        listBox1.Items.Add(((Socket)server.allConnections[i].GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(server.allConnections[i], null)).RemoteEndPoint.ToString());
                    }
                }
            }
            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Text = "OK";
            cmdHandler = new CommandHandler(console, server);
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            cmdHandler.cmd(textBox2.Text);
        }


    }
}
