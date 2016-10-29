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

namespace LiveSplit.LeaderBoard.UI.Components
{
    class RecordComponent : IComponent
    {
        protected SimpleLabel RankLabel { get; set; }
        protected SimpleLabel NameLabel { get; set; }
        protected SimpleLabel TimeLabel { get; set; }

        protected bool NeedUpdate { get; set; }

        public LeaderBoardSettings Settings { get; set; }
        
        public GraphicsCache Cache { get; set; }

        public float PaddingTop => 0f;
        public float PaddingLeft => 0f;
        public float PaddingBottom => 0f;
        public float PaddingRight => 0f;

        public float VerticalHeight => 0;//25 + Settings.SplitHeight;
        public float HorizontalWidth => 0;//Settings.SplitWidth + CalculateLabelsWidth();

        public float MinimumWidth { get; set; }
        public float MinimumHeight { get; set; }

        public IDictionary<string, Action> ContextMenuControls => null;

        public string ComponentName => "Record";

        public RecordComponent(LeaderBoardSettings settings)
        {
            RankLabel = new SimpleLabel();
            NameLabel = new SimpleLabel();
            TimeLabel = new SimpleLabel();

            Settings = settings;
            MinimumHeight = 10;
            NeedUpdate = true;
            Cache = new GraphicsCache();
        }

        public void Dispose(){

        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, System.Drawing.Region clipRegion)
        {
            throw new NotImplementedException();
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, System.Drawing.Region clipRegion)
        {
            throw new NotImplementedException();
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            throw new NotSupportedException();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            throw new NotSupportedException();
        }

        public void SetSettings(XmlNode settings)
        {
            throw new NotSupportedException();
        }

        public string UpdateName
        {
            get { throw new NotSupportedException(); }
        }

        public string XMLURL
        {
            get { throw new NotSupportedException(); }
        }

        public string UpdateURL
        {
            get { throw new NotSupportedException(); }
        }

        public Version Version
        {
            get { throw new NotSupportedException(); }
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
