using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model.flow.item;
using YourMoveApp.cli.api.model.flow.item.container;

namespace YourMoveApp.cli.api.services
{
    interface IFlowTopologyProvider
    {
        public FlowItemContainer GetRootFlowItem();
    }
}
