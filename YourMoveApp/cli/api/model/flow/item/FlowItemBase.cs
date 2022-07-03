using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.cli.api.model.flow.item
{
    public abstract class FlowItemBase
    {
        public delegate void OnEnter();

        public delegate FlowItemResponse ProcessInput(string inputString);

        public delegate void OnExit();

        public abstract string Title { get; }

        public abstract string Display { get; }

        public abstract string Prompt { get; }

        public OnEnter OnEnterDelegate
        {
            get
            {
                return GetOnEnterDelegate();
            }
        }

        public ProcessInput ProcessInputDelegate
        {
            get
            {
                return GetProcessInputDelegate();
            }
        }

        public OnExit OnExitDelegate
        {
            get
            {
                return GetOnExitDelegate();
            }
        }

        protected virtual OnEnter GetOnEnterDelegate()
        {
            return NOP;
        }

        protected abstract ProcessInput GetProcessInputDelegate();

        protected virtual OnExit GetOnExitDelegate()
        {
            return NOP;
        }

        private void NOP()
        {
            // NOP
        }
    }
}
