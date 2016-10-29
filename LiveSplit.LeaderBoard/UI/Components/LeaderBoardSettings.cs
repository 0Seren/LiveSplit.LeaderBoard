using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    public partial class LeaderBoardSettings : UserControl
    {
        public Color TextColor { get; set; }
        public bool OverrideTextColor { get; set; }
        public Color TimeColor { get; set; }
        public bool OverrideTimeColor { get; set; }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString
        {
            get { return BackgroundGradient.ToString(); }
            set { BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value); }
        }

        public LiveSplitState CurrentState { get; set; }
        public bool Display2Rows { get; set; }
        public bool CenteredText { get; set; }

        public bool FilterVariables { get; set; }
        public bool FilterPlatform { get; set; }
        public bool FilterRegion { get; set; }

        public LayoutMode Mode { get; set; }
        public LeaderBoardSettings()
        {
            InitializeComponent();
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }
    }
}
