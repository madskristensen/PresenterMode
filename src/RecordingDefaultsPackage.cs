global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;

namespace PresenterMode
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "Environment", Vsix.Name, 0, 0, true, ProvidesLocalizedCategoryName = false)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "Environment", Vsix.Name, 0, 0, true)]
    [Guid(PackageGuids.RecordingDefaultsString)]
    public sealed class RecordingDefaultsPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
        }
    }
}