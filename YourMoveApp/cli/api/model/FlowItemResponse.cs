using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.cli.api.model
{
    public class FlowItemResponse
    {
        public int ExitPort { get; }
        public string ErrorMessage { get; }

        public bool HasError
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public FlowItemResponse(int exitPort)
            : this(exitPort, string.Empty)
        {
           
        }

        public FlowItemResponse(string errorMessage)
            : this(0, errorMessage)
        {

        }

        private FlowItemResponse(int exitPort, string errorMessage)
        {
            this.ExitPort = exitPort;
            this.ErrorMessage = errorMessage;
        }
    }
}
