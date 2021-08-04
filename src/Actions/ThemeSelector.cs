using System.ComponentModel.Design;
using System.IO;

namespace PresenterMode
{
    public class ThemeSelector
    {
        private const string _darkTheme = "{1ded0138-47ce-435e-84ef-9ec1f439b749}";
        private const string _blueTheme = "{a4d6a176-b948-4b29-8c66-53c97a1ed7d0}";
        private const string _lightTheme = "{de3dbbcd-f642-433c-8353-8f1df4370aba}";

        public static async Task ApplyAsync()
        {
            General settings = await General.GetLiveInstanceAsync();

            if (!settings.AdjustTheme)
            {
                return;
            }

            var theme = settings.Theme switch
            {
                Theme.Dark => _darkTheme,
                Theme.Blue => _blueTheme,
                _ => _lightTheme,
            };

            await VS.StatusBar.ShowMessageAsync("Applying theme...");

            var settingsFile = string.Format(_vsSettings, theme);
            var path = Path.Combine(Path.GetTempPath(), "temp.vssettings");

            System.IO.File.WriteAllText(path, settingsFile);

            await KnownCommands.Tools_ImportandExportSettings.ExecuteAsync($@"/import:""{path}""");
        }

        public const string _vsSettings = @"<UserSettings>
    <ApplicationIdentity version=""16.0""/>
    <ToolsOptions>
        <ToolsOptionsCategory name=""Environment"" RegisteredName=""Environment""/>
    </ToolsOptions>
    <Category name=""Environment_Group"" RegisteredName=""Environment_Group"">
        <Category name=""Environment_FontsAndColors"" Category=""{{1EDA5DD4-927A-43a7-810E-7FD247D0DA1D}}"" Package=""{{DA9FB551-C724-11d0-AE1F-00A0C90FFFC3}}"" RegisteredName=""Environment_FontsAndColors"" PackageName=""Visual Studio Environment Package"">
            <PropertyValue name=""Version"">2</PropertyValue>
            <FontsAndColors Version=""2.0"">
                <Theme Id=""{0}""/>
            </FontsAndColors>
        </Category>
    </Category>
</UserSettings>";
    }
}
