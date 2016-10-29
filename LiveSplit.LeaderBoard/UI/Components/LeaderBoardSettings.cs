using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    public partial class LeaderBoardSettings : UserControl
    {
        /*public Color TextColor { get; set; }
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

        */public LiveSplitState CurrentState { get; set; }/*
        public bool Display2Rows { get; set; }
        public bool CenteredText { get; set; }

        public bool FilterVariables { get; set; }
        public bool FilterPlatform { get; set; }
        public bool FilterRegion { get; set; }*/

        public LayoutMode Mode { get; set; }

        public LeaderBoardSettings()
        {
            InitializeComponent();
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            /*TextColor = SettingsHelper.ParseColor(element["TextColor"]);
            OverrideTextColor = SettingsHelper.ParseBool(element["OverrideTextColor"]);
            TimeColor = SettingsHelper.ParseColor(element["TimeColor"]);
            OverrideTimeColor = SettingsHelper.ParseBool(element["OverrideTimeColor"]);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);
            Display2Rows = SettingsHelper.ParseBool(element["Display2Rows"]);
            CenteredText = SettingsHelper.ParseBool(element["CenteredText"]);
            FilterRegion = SettingsHelper.ParseBool(element["FilterRegion"]);
            FilterPlatform = SettingsHelper.ParseBool(element["FilterPlatform"]);
            FilterVariables = SettingsHelper.ParseBool(element["FilterVariables"]);*/
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") /*^
            SettingsHelper.CreateSetting(document, parent, "TextColor", TextColor) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideTextColor) ^
            SettingsHelper.CreateSetting(document, parent, "TimeColor", TimeColor) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTimeColor", OverrideTimeColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
            SettingsHelper.CreateSetting(document, parent, "Display2Rows", Display2Rows) ^
            SettingsHelper.CreateSetting(document, parent, "CenteredText", CenteredText) ^
            SettingsHelper.CreateSetting(document, parent, "FilterRegion", FilterRegion) ^
            SettingsHelper.CreateSetting(document, parent, "FilterPlatform", FilterPlatform) ^
            SettingsHelper.CreateSetting(document, parent, "FilterVariables", FilterVariables)*/;
        }
    }
}
