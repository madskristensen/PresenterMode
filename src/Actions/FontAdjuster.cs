using System;
using System.IO;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;

namespace PresenterMode
{
    public class FontAdjuster
    {
        public const string CodeLensCategory = "{FC88969A-CBED-4940-8F48-142A503E2381}";
        private const int _fontSize = 10;

        public static async Task ApplyAsync()
        {
            General settings = await General.GetLiveInstanceAsync();

            if (!settings.AdjustFontSize)
            {
                return;
            }

            await VS.StatusBar.ShowMessageAsync("Adjusting fonts...");

            var path = Path.Combine(Path.GetTempPath(), "temp.txt");
            System.IO.File.WriteAllText(path, $"Setting font size to {_fontSize} and zoom level to {settings.ZoomLevel}%");

            // Set font size
            await AdjustFontSizeAsync(FontsAndColorsCategory.TextEditor, _fontSize);
            await AdjustFontSizeAsync(FontsAndColorsCategory.StatementCompletion, _fontSize);
            await AdjustFontSizeAsync(FontsAndColorsCategory.TextOutputToolWindows, _fontSize);
            await AdjustFontSizeAsync(FontsAndColorsCategory.Tooltip, _fontSize);
            await AdjustFontSizeAsync(CodeLensCategory, 9);

            // Open document to set zoom level
            DocumentView doc = await VS.Documents.OpenAsync(path);
            doc.TextView.ZoomLevel = settings.ZoomLevel;

            // This will apply the zoom level to all open documents.
            EnvDTE80.DTE2 dte = await VS.GetRequiredServiceAsync<EnvDTE.DTE, EnvDTE80.DTE2>();
            dte.ExecuteCommand("View.ZoomIn");
            dte.ExecuteCommand("View.ZoomOut");

            // And close the document when done
            await doc?.WindowFrame.CloseFrameAsync(FrameCloseOption.NoSave);
        }

        public static async Task AdjustFontSizeAsync(string categoryGuid, ushort fontSize)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IVsFontAndColorStorage storage = await VS.Services.GetFontAndColorStorageAsync();
            Assumes.Present(storage);

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (storage is not IVsFontAndColorUtilities utilities)
            {
                return;
            }

            var pLOGFONT = new LOGFONTW[1];
            var pInfo = new FontInfo[1];
            var category = new Guid(categoryGuid);

            ErrorHandler.ThrowOnFailure(storage.OpenCategory(category, (uint)(__FCSTORAGEFLAGS.FCSF_LOADDEFAULTS | __FCSTORAGEFLAGS.FCSF_PROPAGATECHANGES)));
            try
            {
                if (!ErrorHandler.Succeeded(storage.GetFont(pLOGFONT, pInfo)))
                {
                    return;
                }

                pInfo[0].wPointSize = fontSize;
                ErrorHandler.ThrowOnFailure(storage.SetFont(pInfo));
                ErrorHandler.ThrowOnFailure(utilities.FreeFontInfo(pInfo));
            }
            finally
            {
                storage.CloseCategory();
            }
        }
    }
}
