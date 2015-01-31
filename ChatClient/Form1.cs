using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChatUtils;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        TCPClient client;
        ChatUtils.Console console;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.SendMessage(cmdBox.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Send";
            button2.Text = "Connect";
            timer1.Start();
            console = new ChatUtils.Console();
            button1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            consoleBox.Text = console.getText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client = new TCPClient(ipBox.Text, 3000, console);
            button1.Enabled = true;
        }
    }
}
