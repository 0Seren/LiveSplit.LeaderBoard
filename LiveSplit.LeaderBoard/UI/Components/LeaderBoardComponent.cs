using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using LiveSplit.Web.Share;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SpeedrunComSharp;
using System.Collections.ObjectModel;
using LiveSplit.Options;

namespace LiveSplit.LeaderBoard.UI.Components {
    class LeaderBoardComponent : IComponent
    {
        protected InfoTextComponent InternalComponent { get; set; }

        protected LeaderBoardSettings Settings { get; set; }

        private GraphicsCache Cache { get; set; }
        private ITimeFormatter TimeFormatter { get; set; }
        private RegularTimeFormatter LocalTimeFormatter { get; set; }
        private LiveSplitState State { get; set; }
        private TimeStamp LastUpdate { get; set; }
        private TimeSpan RefreshInterval { get; set; }
        public Leaderboard SRCLeaderBoard { get; protected set; }
        private bool IsLoading { get; set; }
        private SpeedrunComClient Client { get; set; }

        public string ComponentName => "Leader Board";

        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingRight => InternalComponent.PaddingRight;

        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float MinimumWidth => InternalComponent.MinimumWidth;
        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumHeight => InternalComponent.MinimumHeight;

        public IDictionary<string, Action> ContextMenuControls => null;

        public LeaderBoardComponent(LiveSplitState state)
        {
            State = state;
            Client = new SpeedrunComClient(userAgent: Updates.UpdateHelper.UserAgent, maxCacheElements: 0);
            RefreshInterval = TimeSpan.FromMinutes(5);
            Cache = new GraphicsCache();
            TimeFormatter = new AutomaticPrecisionTimeFormatter();
            LocalTimeFormatter = new RegularTimeFormatter();
            InternalComponent = new InfoTextComponent("Leader Board", "-");

            Settings = new LeaderBoardSettings()
            {
                CurrentState = state
            };
        }

        public void Dispose()
        {

        }

        private void RefreshLeaderBoard()
        {
            LastUpdate = TimeStamp.Now;
            SRCLeaderBoard = null;

            try
            {
                if (State != null && State.Run != null && State.Run.Metadata.Game != null && State.Run.Metadata.Category != null)
                {
                    //Get these from setings later:
                    var region_filter = false ? "" : null;
                    var platorm_filter = false ? "" : null;
                    IEnumerable<SpeedrunComSharp.VariableValue> variable_filter = null;
                    EmulatorsFilter emulator_filter = EmulatorsFilter.NotSet;

                    var leaderboard = Client.Leaderboards.GetLeaderboardForFullGameCategory(State.Run.Metadata.Game.ID, State.Run.Metadata.Category.ID,
                            platformId: platorm_filter,
                            regionId: region_filter,
                            emulatorsFilter: emulator_filter,
                            variableFilters: variable_filter);

                    if (leaderboard != null)
                    {
                        SRCLeaderBoard = leaderboard;
                    }
                }
            } catch(Exception ex)
            {
                Log.Error(ex);
            }
            IsLoading = false;
            ShowLeaderBoard();
        }

        private void ShowLeaderBoard()
        {
            if(SRCLeaderBoard != null)
            {
                //Settings:
                var always_show_first = true;
                var num_to_show = 5;
                var centered_around_me = false;

                var timingMethod = State.CurrentTimingMethod;
                var game = State.Run.Metadata.Game;
                var records = SRCLeaderBoard.Records;

                if (game != null)
                {
                    timingMethod = game.Ruleset.DefaultTimingMethod.ToLiveSplitTimingMethod();
                    LocalTimeFormatter.Accuracy = game.Ruleset.ShowMilliseconds ? TimeAccuracy.Hundredths : TimeAccuracy.Seconds;
                }

                var isLoggedIn = SpeedrunCom.Client.IsAccessTokenValid;
                var userName = string.Empty;
                if (isLoggedIn)
                {
                    userName = SpeedrunCom.Client.Profile.Name;
                }

                var title_text = "Rank\tPlayer\tTime";
                
            } else if (IsLoading)
            {
                InternalComponent.InformationName = "Loading Leader Board...";
                InternalComponent.AlternateNameText = new[] { "Loading LB..." };
            } else
            {
                InternalComponent.InformationName = "Unknown Leader Board";
                InternalComponent.AlternateNameText = new[] { "Unknown LB" };
            }
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Cache.Restart();
            Cache["Game"] = state.Run.GameName;
            Cache["Category"] = state.Run.CategoryName;
            //Cache["PlatformID"] = Settings.FilterPlatform ? state.Run.Metadata.PlatformName : null;
            //Cache["RegionID"] = Settings.FilterRegion ? state.Run.Metadata.RegionName : null;
            //Cache["UsesEmulator"] = Settings.FilterPlatform ? (bool?)state.Run.Metadata.UsesEmulator : null;
            //Cache["Variables"] = Settings.FilterVariables ? string.Join(",", state.Run.Metadata.VariableValueNames.Values) : null;

            if (Cache.HasChanged)
            {
                IsLoading = true;
                SRCLeaderBoard = null;
                ShowLeaderBoard();
                Task.Factory.StartNew(RefreshLeaderBoard);
            }
            else if (LastUpdate != null && TimeStamp.Now - LastUpdate >= RefreshInterval)
            {
                Task.Factory.StartNew(RefreshLeaderBoard);
            }
            else
            {
                /*Cache["CenteredText"] = Settings.CenteredText && !Settings.Display2Rows;
                Cache["RealPBTime"] = GetPBTime(Model.TimingMethod.RealTime);
                Cache["GamePBTime"] = GetPBTime(Model.TimingMethod.GameTime);*/

                if (Cache.HasChanged)
                {
                    ShowLeaderBoard();
                }
            }

            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        private void DrawBackground(Graphics g, LiveSplitState state, float width, float height)
        {
            if (Settings.BackgroundColor.ToArgb() != Color.Transparent.ToArgb()
                || Settings.BackgroundGradient != GradientType.Plain
                && Settings.BackgroundColor2.ToArgb() != Color.Transparent.ToArgb())
            {
                var gradientBrush = new LinearGradientBrush(
                            new PointF(0, 0),
                            Settings.BackgroundGradient == GradientType.Horizontal
                            ? new PointF(width, 0)
                            : new PointF(0, height),
                            Settings.BackgroundColor,
                            Settings.BackgroundGradient == GradientType.Plain
                            ? Settings.BackgroundColor
                            : Settings.BackgroundColor2);
                g.FillRectangle(gradientBrush, 0, 0, width, height);
            }
        }

        private void PrepareDraw(LiveSplitState state, LayoutMode mode)
        {

        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, System.Drawing.Region clipRegion)
        {

        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, System.Drawing.Region clipRegion)
        {

        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();

    }
}
