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
        }

        public string getText()
        {
            return Text;
        }
    }
}
