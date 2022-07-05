using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.cli.api.model.flow.item
{
    class SimpleMenuFlowItem : FlowItemBase
    {
        private readonly string _title;

        private readonly string _display;

        private readonly string _prompt;

        private SimpleMenuFlowItem(string title, Dictionary<int, string> exitPortToMenuLine, string prompt)
        {
            this._title = title;
            this._display = ConvertToDisplay(exitPortToMenuLine);
            this._prompt = prompt;
        }

        public override string Title => _title;

        public override string Display => _display;

        public override string Prompt => _prompt;

        protected override ProcessInput GetProcessInputDelegate()
        {
            return ProcessInputInternal;
        }

        private string ConvertToDisplay(Dictionary<int, string> exitPortToMenuLine)
        {
            StringBuilder displayStringBuilder = new StringBuilder();
            List<int> sortedExitPorts = new List<int>(exitPortToMenuLine.Keys);
            sortedExitPorts.Sort();
            foreach (int exitPort in sortedExitPorts)
            {
                displayStringBuilder.Append(exitPort + " - " + exitPortToMenuLine[exitPort] + "\n");
            }
            return displayStringBuilder.ToString();
        }

        private FlowItemResponse ProcessInputInternal(string inputString)
        {
            try
            {
                return new FlowItemResponse(Int32.Parse(inputString));
            }
            catch(Exception e)
            {
                return new FlowItemResponse(e.Message);
            }
        }

        public class Builder
        {
            private string _title = = string.Empty;
            private Dictionary<int, string> _exitPortToMenuLine = new Dictionary<int, string>();
            private string _prompt = string.Empty;

            public Builder (string title)
            {
                this._title = title;
            }

            public Builder WithPrompt(string prompt)
            {
                this._prompt = prompt;
                return this;
            }

            public Builder WithMenuLineAtExitPort(string menuLine, int exitPort)
            {
                this._exitPortToMenuLine.Add(exitPort, menuLine);
                return this;
            }

            public SimpleMenuFlowItem Build()
            {
                return new SimpleMenuFlowItem(_title, _exitPortToMenuLine, _prompt);
            }
        }
    }
}
