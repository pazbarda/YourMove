using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.cli.api.model.flow.item;
using YourMoveApp.cli.api.model.flow.item.container;
using YourMoveApp.cli.api.services;


namespace YourMoveApp.cli.services
{

    public class DemoFlowTopologyProvider : IFlowTopologyProvider
    {
        private readonly FlowItemContainer _rootFlowItemContainer = BuildFlowAndGetRoot();

        public FlowItemContainer GetRootFlowItem()
        {
            return _rootFlowItemContainer;
        }

        private static FlowItemContainer BuildFlowAndGetRoot()
        {
            FlowItemContainer mainMenuContainer = new FlowItemContainer(
                    new SimpleMenuFlowItem.Builder("MAIN MENU")
                        .WithPrompt("Please choose from the above options")
                        .WithMenuLineAtExitPort("Screen1", 1)
                        .WithMenuLineAtExitPort("Screen2", 2)
                        .Build());
            FlowItemContainer screen1Container = new FlowItemContainer(
                    new SimpleMenuFlowItem.Builder("SCREEN 1")
                     .WithPrompt("Please choose from the above options")
                     .WithMenuLineAtExitPort("Main Menu", 0)
                     .WithMenuLineAtExitPort("Screen2", 2)
                     .Build()
                );
            FlowItemContainer screen2Container = new FlowItemContainer(
                    new SimpleMenuFlowItem.Builder("SCREEN 2")
                     .WithPrompt("Please choose from the above options")
                     .WithMenuLineAtExitPort("Main Menu", 0)
                     .WithMenuLineAtExitPort("Screen1", 1)
                     .Build()
                );
            mainMenuContainer.AddFlowItemContainerAtExitPort(screen1Container, 1);
            mainMenuContainer.AddFlowItemContainerAtExitPort(screen2Container, 2);
            screen1Container.AddFlowItemContainerAtExitPort(mainMenuContainer, 0);
            screen1Container.AddFlowItemContainerAtExitPort(screen2Container, 2);
            screen2Container.AddFlowItemContainerAtExitPort(mainMenuContainer, 0);
            screen2Container.AddFlowItemContainerAtExitPort(screen1Container, 1);
            return mainMenuContainer;
        }
    }
}
