using NetSparkleUpdater.Events;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUpdater.UI.Console
{
    /// <summary>
    /// A progress bar
    /// </summary>
    public class DownloadProgress : IDownloadProgress
    {
        private ProgressBar progressBar;

        /// <summary>
        /// Event to fire when the download UI is complete; tells you 
        /// if the install process should happen or not
        /// </summary>
        public event DownloadInstallEventHandler DownloadProcessCompleted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">The appcast item to use</param>
        public DownloadProgress(AppCastItem item)
        {
            System.Console.Write($"Downloading {item.AppName} {item.Version}...");
            this.progressBar = new ProgressBar();
        }

        /// <summary>
        /// Close UI
        /// </summary>
        public void Close()
        {
            this.progressBar?.Dispose();
        }

        /// <summary>
        /// Display an error message
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        public bool DisplayErrorMessage(string errorMessage)
        {
            System.Console.WriteLine($"ERROR: {errorMessage}");
            return true;
        }

        /// <summary>
        /// Update UI to show file is downloaded and signature check result
        /// </summary>
        public void FinishedDownloadingFile(bool isDownloadedFileValid)
        {
            this.progressBar?.Dispose();
            System.Console.WriteLine(" DONE!");

            if (isDownloadedFileValid)
            {
                System.Console.WriteLine("Application will now close for upgrade.");
                DownloadProcessCompleted?.Invoke(this, new DownloadInstallEventArgs(true));
            }
            else
            {
                System.Console.WriteLine("Invalid download");
                DownloadProcessCompleted?.Invoke(this, new DownloadInstallEventArgs(false));
            }
        }

        /// <summary>
        /// Event called when the client download progress changes
        /// </summary>
        public void OnDownloadProgressChanged(object sender, ItemDownloadProgressEventArgs args)
        {
            this.progressBar?.Report(args.ProgressPercentage);
        }

        /// <summary>
        /// Enables or disables the "Install and Relaunch" button
        /// </summary>
        public void SetDownloadAndInstallButtonEnabled(bool shouldBeEnabled)
        {
        }

        /// <summary>
        /// Show the UI
        /// </summary>
        public void Show(bool isOnMainThread)
        {
        }
    }
}
