using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using System.Text.RegularExpressions;
using LiveSplit.Web.Share;

namespace LiveSplit.UI.Components
{
    public partial class LeaderBoardSettings : UserControl
    {
        public Color HeaderColor { get; set; }
        public bool OverrideHeaderColor { get; set; }

        public Color TimeColor { get; set; }
        public bool OverrideTimeColor { get; set; }

        public Color RankColor { get; set; }
        public bool OverrideRankColor { get; set; }

        public Color NameColor { get; set; }
        public enum NameThemeChoice {
            SRC_Dark_Background,
            SRC_Light_Background,
            Custom_Solid_Color_Theme
        };
        public NameThemeChoice NameChoice { get; set; }
        public string NameThemeString
        {
            get { return Regex.Replace(NameChoice.ToString(), @"[_]", " "); }
            set { NameChoice = (NameThemeChoice)Enum.Parse(typeof(NameThemeChoice), Regex.Replace(value, @"[\s]", "_")); }
        }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString
        {
            get { return BackgroundGradient.ToString(); }
            set { BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value); }
        }

        public int MaxPositions { get; set; }
        public bool SelfCentered { get; set; }
        public bool ShowFirst { get; set; }

        public bool FilterEmulator { get; set; }
        public bool FilterPlatform { get; set; }
        public bool FilterRegion { get; set; }

        public LiveSplitState CurrentState { get; set; }

        public LayoutMode Mode { get; set; }

        public LeaderBoardSettings()
        {
            InitializeComponent();

            HeaderColor = Color.FromArgb(255, 255, 255);
            OverrideHeaderColor = false;
            TimeColor = Color.FromArgb(255, 255, 255);
            OverrideTimeColor = false;
            RankColor = Color.FromArgb(255, 255, 255);
            OverrideRankColor = false;
            NameColor = Color.FromArgb(255, 255, 255);
            NameChoice = NameThemeChoice.SRC_Dark_Background;

            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;

            MaxPositions = 3;
            SelfCentered = false;
            ShowFirst = true;

            FilterEmulator = false;
            FilterPlatform = false;
            FilterRegion = false;

            //Text Colors:
            chkbxOverrideHeaderLayoutSettings.DataBindings.Add("Checked", this, "OverrideHeaderColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnHeaderColor.DataBindings.Add("BackColor", this, "HeaderColor", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxOverrideTimeLayoutSettings.DataBindings.Add("Checked", this, "OverrideTimeColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnTimeColor.DataBindings.Add("BackColor", this, "TimeColor", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxOverrideRankLayoutSettings.DataBindings.Add("Checked", this, "OverrideRankColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnRankColor.DataBindings.Add("BackColor", this, "RankColor", false, DataSourceUpdateMode.OnPropertyChanged);

            //Name Colors:
            cmbNameTheme.DataBindings.Add("SelectedItem", this, "NameThemeString", false, DataSourceUpdateMode.OnPropertyChanged);
            btnNameColor.DataBindings.Add("BackColor", this, "NameColor", false, DataSourceUpdateMode.OnPropertyChanged);

            //Background Colors:
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);

            //Leaderboard Settings
            nmbrMaxPositions.DataBindings.Add("Value", this, "MaxPositions", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxSelfCentered.DataBindings.Add("Checked", this, "SelfCentered", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxShowFirst.DataBindings.Add("Checked", this, "ShowFirst", false, DataSourceUpdateMode.OnPropertyChanged);

            //Filters:
            chkbxFilterEmulator.DataBindings.Add("Checked", this, "FilterEmulator", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxFilterPlatform.DataBindings.Add("Checked", this, "FilterPlatform", false, DataSourceUpdateMode.OnPropertyChanged);
            chkbxFilterRegion.DataBindings.Add("Checked", this, "FilterRegion", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        void chkOverrideHeaderColor_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = btnHeaderColor.Enabled = chkbxOverrideHeaderLayoutSettings.Checked;
        }

        void chkOverrideRankColor_CheckedChanged(object sender, EventArgs e)
        {
            label2.Enabled = btnRankColor.Enabled = chkbxOverrideRankLayoutSettings.Checked;
        }

        void chkOverrideTimeColor_CheckedChanged(object sender, EventArgs e)
        {
            label3.Enabled = btnTimeColor.Enabled = chkbxOverrideTimeLayoutSettings.Checked;
        }

        /*void chkOverrideNameColor_CheckedChanged(object sender, EventArgs e)
        {
            btnNameColor.Visible = cmbNameTheme.SelectedText == "Custom Solid Color Theme";
        }*/

        void chkSelfCentered_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkbxSelfCentered.Checked)
            {
                chkbxShowFirst.Enabled = false;
                chkbxShowFirst.Checked = true;
            } else
            {
                chkbxShowFirst.Enabled = true;
            }
        }

        void LeaderBoardSettings_Load(object sender, EventArgs e)
        {
            chkOverrideHeaderColor_CheckedChanged(null, null);
            chkOverrideRankColor_CheckedChanged(null, null);
            chkOverrideTimeColor_CheckedChanged(null, null);
            //chkOverrideNameColor_CheckedChanged(null, null);
            chkSelfCentered_CheckedChanged(null, null);

            if (SpeedrunCom.Client.IsAccessTokenValid && SpeedrunCom.Client.Profile.Name != string.Empty)
            {
                label6.Visible = false;
                chkbxSelfCentered.Enabled = true;
            } else
            {
                label6.Visible = true;
                chkbxSelfCentered.Enabled = false;
                chkbxSelfCentered.Checked = false;
            }
        }

        void cmbGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        void cmbNameTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNameColor.Visible = cmbNameTheme.SelectedItem.ToString() == "Custom Solid Color Theme";
            NameThemeString = cmbNameTheme.SelectedItem.ToString();
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            HeaderColor = SettingsHelper.ParseColor(element["HeaderColor"]);
            OverrideHeaderColor = SettingsHelper.ParseBool(element["OverrideHeaderColor"]);

            RankColor = SettingsHelper.ParseColor(element["RankColor"]);
            OverrideRankColor = SettingsHelper.ParseBool(element["OverrideRankColor"]);

            TimeColor = SettingsHelper.ParseColor(element["TimeColor"]);
            OverrideTimeColor = SettingsHelper.ParseBool(element["OverrideTimeColor"]);

            NameColor = SettingsHelper.ParseColor(element["NameColor"]);
            NameThemeString = SettingsHelper.ParseString(element["NameChoice"]);

            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);

            MaxPositions = SettingsHelper.ParseInt(element["MaxPositions"]);
            SelfCentered = SettingsHelper.ParseBool(element["SelfCentered"]);
            ShowFirst = SettingsHelper.ParseBool(element["ShowFirst"]);

            FilterRegion = SettingsHelper.ParseBool(element["FilterRegion"]);
            FilterPlatform = SettingsHelper.ParseBool(element["FilterPlatform"]);
            FilterEmulator = SettingsHelper.ParseBool(element["FilterEmulator"]);
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
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
            SettingsHelper.CreateSetting(document, parent, "HeaderColor", HeaderColor) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideHeaderColor", OverrideHeaderColor) ^
            SettingsHelper.CreateSetting(document, parent, "RankColor", RankColor) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideRankColor", OverrideRankColor) ^
            SettingsHelper.CreateSetting(document, parent, "TimeColor", TimeColor) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTimeColor", OverrideTimeColor) ^
            SettingsHelper.CreateSetting(document, parent, "NameColor", NameColor) ^
            SettingsHelper.CreateSetting(document, parent, "NameChoice", NameChoice) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
            SettingsHelper.CreateSetting(document, parent, "MaxPositions", MaxPositions) ^
            SettingsHelper.CreateSetting(document, parent, "SelfCentered", SelfCentered) ^
            SettingsHelper.CreateSetting(document, parent, "ShowFirst", ShowFirst) ^
            SettingsHelper.CreateSetting(document, parent, "FilterRegion", FilterRegion) ^
            SettingsHelper.CreateSetting(document, parent, "FilterPlatform", FilterPlatform) ^
            SettingsHelper.CreateSetting(document, parent, "FilterEmulator", FilterEmulator);
        }
    }
}
