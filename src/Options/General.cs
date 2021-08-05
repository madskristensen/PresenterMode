using System.ComponentModel;
using Community.VisualStudio.Toolkit;

namespace PresenterMode
{
    internal partial class OptionsProvider
    {
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("Font size")]
        [DisplayName("Enable font size adjustment")]
        [Description("Changes the font size")]
        [DefaultValue(true)]
        public bool AdjustFontSize { get; set; } = true;

        [Category("Font size")]
        [DisplayName("Zoom level")]
        [Description("Changes the zoom level of the editor. Default is 140.")]
        [DefaultValue(140)]
        public int ZoomLevel { get; set; } = 140;

        [Category("Theme")]
        [DisplayName("Enable theme selection")]
        [Description("Changes the theme")]
        [DefaultValue(true)]
        public bool AdjustTheme { get; set; } = true;

        [Category("Theme")]
        [DisplayName("Theme")]
        [Description("Determines what theme to apply")]
        [DefaultValue(Theme.Dark)]
        public Theme Theme { get; set; } = Theme.Dark;

        [Category("Windows")]
        [DisplayName("Close tool windows")]
        [Description("Closes all tool windows but the main ones.")]
        [DefaultValue(true)]
        public bool CloseWindows { get; set; } = true;

        [Category("Resize")]
        [DisplayName("Enable window resizing")]
        [Description("Resizes the main window of Visual Studio.")]
        [DefaultValue(true)]
        public bool ResizeVisualStudio { get; set; } = true;

        [Category("Resize")]
        [DisplayName("Width")]
        [Description("The width in pixels of the main window. Default is 1920.")]
        [DefaultValue(1920)]
        public int Width { get; set; } = 1920;

        [Category("Resize")]
        [DisplayName("Height")]
        [Description("The height in pixels of the main window. Default is 1080")]
        [DefaultValue(1080)]
        public int Height { get; set; } = 1080;
    }

    public enum Theme
    {
        Blue,
        Dark,
        Light,
    }
}
