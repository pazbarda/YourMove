using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model;

namespace YourMoveApp.cli.api.services
{
    interface IFlowDriver
    {
        public CLIResponse processInput(string inputString);
    }
}
