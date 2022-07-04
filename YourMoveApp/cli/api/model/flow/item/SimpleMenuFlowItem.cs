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

        public SimpleMenuFlowItem(string title, Dictionary<int, string> exitPortToMenuLine, string prompt)
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
            foreach (int exitPort in exitPortToMenuLine.Keys)
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
    }
}
