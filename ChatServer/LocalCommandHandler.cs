using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class LocalCommandHandler
    {
        ChatUtils.Console console;
        public bool on = true;

        public LocalCommandHandler(ChatUtils.Console console)
        {
            this.console = console;
        }

        public void cmd(string cmd)
        {
            cmd = cmd.TrimStart('/');
            if (cmd.ToUpper() == "STOP")
            {
                on = false;
            }
        }
    }
}
