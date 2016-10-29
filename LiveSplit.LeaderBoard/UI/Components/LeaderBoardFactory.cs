using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(LiveSplit.LeaderBoard.UI.Components.LeaderBoardFactory))]

namespace LiveSplit.LeaderBoard.UI.Components {

    class LeaderBoardFactory : IComponentFactory {

        public string ComponentName => "Leader Board";

        public string Description => "Shows the Leader Board for the run";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new LeaderBoardComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/update.LiveSplit.LeaderBoard.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("1.0.0");
    }
}
