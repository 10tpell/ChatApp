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
            client.SendMessage(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            console = new ChatUtils.Console();
            client = new TCPClient("192.168.1.154", 3000, console);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Text = console.getText();
        }
    }
}
