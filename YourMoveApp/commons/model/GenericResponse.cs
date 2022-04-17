using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class GenericResponse
    {
        public Boolean Success { get; }
         public String Message { get; }

        public GenericResponse(Boolean success, String message)
        {
            this.Success = success;
            this.Message = message;
        }

    }
}
