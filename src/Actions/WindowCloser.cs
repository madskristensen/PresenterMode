using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;

namespace PresenterMode
{
    public class WindowCloser
    {
        private static readonly Dictionary<Guid, bool> _windowGuidsToKeep = new()
        {
            { new Guid(WindowGuids.SolutionExplorer), true },
            { new Guid(WindowGuids.GitChanges), true },
            { new Guid(WindowGuids.GitRepository), true },
            { new Guid(WindowGuids.ErrorList), false },
            { new Guid(WindowGuids.OutputWindow), false }
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
                if (!_windowGuidsToKeep.ContainsKey(window.Guid))
                {
                    await window.HideAsync();
                }
            }
        }
    }
}
