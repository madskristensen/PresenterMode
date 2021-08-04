namespace PresenterMode
{
    [Command(PackageIds.SetDefaults)]
    internal sealed class SetDefaultsCommand : BaseCommand<SetDefaultsCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();

            await WindowResizer.ApplyAsync();
            await Task.Yield();

            await FontAdjuster.ApplyAsync();
            await Task.Yield();

            await ThemeSelector.ApplyAsync();
            await Task.Yield();

            await WindowCloser.ApplyAsync();
            await Task.Yield();

            await VS.StatusBar.ShowMessageAsync("Presenter Mode applied.");
        }
    }
}
