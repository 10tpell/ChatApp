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

namespace ChatServer
{
    public partial class Form1 : Form
    {
        ChatUtils.Console console;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            console = new ChatUtils.Console();
            TCPServer server = new TCPServer(console);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = console.getText();
        }


    }
}
