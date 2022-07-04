using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model;
using YourMoveApp.cli.api.model.flow.item;

namespace YourMoveApp.cli.api.services
{
    interface IFlowItemProcessor
    {
        public FlowItemResponse ProcessFlowItem(FlowItemBase flowItemBase, string inputString);
    }
}
