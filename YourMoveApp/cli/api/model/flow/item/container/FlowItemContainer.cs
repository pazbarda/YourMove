using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model.flow.item;

namespace YourMoveApp.cli.api.model.flow.item.container
{
    public class FlowItemContainer
    {
        public FlowItemBase FlowItemBase { get; }

        private Dictionary<int, FlowItemContainer> _exitPortToFlowItemBase = new Dictionary<int, FlowItemContainer>();

        public FlowItemContainer(FlowItemBase flowItemBase)
        {
            this.FlowItemBase = flowItemBase;
        }

        public void AddFlowItemContainerAtExitPort(FlowItemContainer flowItemContainer, int exitPort)
        {
            _exitPortToFlowItemBase.Add(exitPort, flowItemContainer);
        }

        public FlowItemContainer GetFlowItemBaseAtExitPort(int exitPort)
        {
            return _exitPortToFlowItemBase[exitPort];
        }
    }
}
