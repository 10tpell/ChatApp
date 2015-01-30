using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUtils
{
    public class Console
    {
        string Text;

        public void writeLine(string text)
        {
            this.Text += System.Environment.NewLine + text;
            System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "log.txt");
            file.WriteLine(text + System.Environment.NewLine);
            System.Console.WriteLine("Outputting log file to: " + System.IO.Directory.GetCurrentDirectory());
            file.Close();
        }

        public string getText()
        {
            return Text;
        }
    }
}
