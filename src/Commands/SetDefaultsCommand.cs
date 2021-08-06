using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace PresenterMode
{
    [Command(PackageIds.SetDefaults)]
    internal sealed class SetDefaultsCommand : BaseCommand<SetDefaultsCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            
            if (VsShellUtilities.IsSolutionBuilding(Package) ||
                VsShellUtilities.GetDebugMode(Package) != DBGMODE.DBGMODE_Design)
            {
                await VS.MessageBox.ShowAsync("You can't change to Presenter Mode while in run or debug mode.");
                return;
            }

            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();

            await WindowResizer.ApplyAsync();
            await Task.Yield();

            await FontAdjuster.ApplyAsync();
            await Task.Yield();

            await ThemeSelector.ApplyAsync();
            await Task.Yield();

            await WindowCloser.ApplyAsync();
            await Task.Yield();

            await VS.StatusBar.ClearAsync();
        }
    }
}
