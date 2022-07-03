using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.cli.api.model
{
    public class CLIResponse
    {
        public string Title { get; }

        public string Display { get; }

        public string Prompt { get; }

        public string ErrorMessage { get; }

        public bool HasError {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public CLIResponse(string title, string display, string prompt) 
            : this(title, display, prompt, string.Empty)
        {
        }

        public CLIResponse(string errorMessage)
            : this(string.Empty, string.Empty, string.Empty, errorMessage)
        {
        }

        private CLIResponse(string title, string display, string prompt, string errorMessage)
        {
            this.Title = title;
            this.Display = display;
            this.Prompt = prompt;
            this.ErrorMessage = errorMessage;
        }
    }
}
