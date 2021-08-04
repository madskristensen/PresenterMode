using System.Collections.Generic;
using System.Linq;

namespace PresenterMode
{
    public class WindowCloser
    {
        private static readonly Guid[] _windowGuidsToKeep = new[] {
           new Guid(WindowGuids.SolutionExplorer),
           new Guid(WindowGuids.GitChanges),
           new Guid(WindowGuids.GitRepository),
           new Guid(WindowGuids.ErrorList),
           new Guid(WindowGuids.OutputWindow)
        };

        public static async Task ApplyAsync()
        {
            General settings = await General.GetLiveInstanceAsync();

            if (!settings.CloseWindows)
            {
                return;
            }

            await VS.StatusBar.ShowMessageAsync("Hiding tool windows...");

            IEnumerable<WindowFrame> windows = await VS.Windows.GetAllWindowsAsync();

            foreach (WindowFrame window in windows)
            {
                if (!_windowGuidsToKeep.Contains(window.Guid))
                {
                    await window.CloseFrameAsync(FrameCloseOption.NoSave);
                }
            }
        }
    }
}
