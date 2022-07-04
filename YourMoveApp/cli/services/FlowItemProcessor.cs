using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model;
using YourMoveApp.cli.api.model.flow.item;
using YourMoveApp.cli.api.services;

namespace YourMoveApp.cli.services
{
    class FlowItemProcessor : IFlowItemProcessor
    {
        public FlowItemResponse ProcessFlowItem(FlowItemBase flowItemBase, string inputString)
        {
            flowItemBase.OnEnterDelegate();
            FlowItemResponse flowItemResponse = flowItemBase.ProcessInputDelegate(inputString);
            if (!flowItemResponse.HasError)
            {
                flowItemBase.OnExitDelegate();
            }
            return flowItemResponse;
        }
    }
}
