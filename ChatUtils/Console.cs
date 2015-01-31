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
        System.IO.StreamWriter file;

        public Console()
        {
            try
            {
                file = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "log.txt");
            }
            catch
            {

            }
        }

        public void writeLine(string text)
        {
            this.Text += System.Environment.NewLine + text;
            try
            {
                file.WriteLine(text + System.Environment.NewLine);
            }
            catch
            {

            }
            System.Console.WriteLine("Outputting log file to: " + System.IO.Directory.GetCurrentDirectory());
            file.Close();
        }

        public string getText()
        {
            return Text;
        }
    }
}
