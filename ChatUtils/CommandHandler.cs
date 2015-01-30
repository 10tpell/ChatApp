using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUtils
{
    public class CommandHandler
    {
        string Command;
        Console console;

        public CommandHandler(string cmd, Console con)
        {
            this.Command = cmd;
            this.console = con;
            this.cmd();
        }

        private void cmd()
        {
            console.writeLine("User used:" + Command.TrimStart('/') + " Command");
        }
    }
}
