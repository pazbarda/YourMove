using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.server.api.exceptions
{
    internal class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(String message): base(message)
        { 
        }
    }
}
