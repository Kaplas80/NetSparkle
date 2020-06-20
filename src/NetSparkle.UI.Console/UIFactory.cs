using System;
using System.Collections.Generic;
using NetSparkleUpdater.Interfaces;
using NetSparkleUpdater.Properties;

namespace NetSparkleUpdater.UI.Console
{
    /// <summary>
    /// UI factory for console interface
    /// </summary>
    public class UIFactory : IUIFactory
    {
        public UIFactory()
        {
            HideReleaseNotes = false;
            HideRemindMeLaterButton = false;
            HideSkipButton = false;
        }

        /// <summary>
        /// Hides the release notes view when an update is found.
        /// </summary>
        public bool HideReleaseNotes { get; set; }

        /// <summary>
        /// Hides the skip this update button when an update is found.
        /// </summary>
        public bool HideSkipButton { get; set; }

        /// <summary>
        /// Hides the remind me later button when an update is found.
        /// </summary>
        public bool HideRemindMeLaterButton { get; set; }

        /// <summary>
        /// Create sparkle form implementation
        /// </summary>
        /// <param name="sparkle">The <see cref="SparkleUpdater"/> instance to use</param>
        /// <param name="updates">Sorted array of updates from latest to earliest</param>
        /// <param name="isUpdateAlreadyDownloaded">If true, make sure UI text shows that the user is about to install the file instead of download it.</param>
        public virtual IUpdateAvailable CreateUpdateAvailableWindow(SparkleUpdater sparkle, List<AppCastItem> updates, bool isUpdateAlreadyDownloaded = false)
        {
            return new UpdateAvailable(sparkle, updates);
        }

        /// <summary>
        /// Create download progress window
        /// </summary>
        /// <param name="item">Appcast item to download</param>
        public virtual IDownloadProgress CreateProgressWindow(AppCastItem item)
        {
            return new DownloadProgress(item);
        }

        /// <summary>
        /// Inform user in some way that NetSparkle is checking for updates
        /// </summary>
        public virtual ICheckingForUpdates ShowCheckingForUpdates()
        {
            return new CheckingForUpdates();
        }

        /// <summary>
        /// Initialize UI. Called when Sparkle is constructed and/or when the UIFactory is set.
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// Show user a message saying downloaded update format is unknown
        /// </summary>
        /// <param name="downloadFileName">The filename to be inserted into the message text</param>
        public virtual void ShowUnknownInstallerFormatMessage(string downloadFileName)
        {
            ShowMessage(string.Format(Resources.DefaultUIFactory_ShowUnknownInstallerFormatMessageText, downloadFileName));
        }

        /// <summary>
        /// Show user that current installed version is up-to-date
        /// </summary>
        public virtual void ShowVersionIsUpToDate()
        {
            ShowMessage(Resources.DefaultUIFactory_ShowVersionIsUpToDateMessage);
        }

        /// <summary>
        /// Show message that latest update was skipped by user
        /// </summary>
        public virtual void ShowVersionIsSkippedByUserRequest()
        {
            ShowMessage(Resources.DefaultUIFactory_ShowVersionIsSkippedByUserRequestMessage);
        }

        /// <summary>
        /// Show message that appcast is not available
        /// </summary>
        /// <param name="appcastUrl">the URL for the appcast file</param>
        public virtual void ShowCannotDownloadAppcast(string appcastUrl)
        {
            ShowMessage(Resources.DefaultUIFactory_ShowCannotDownloadAppcastMessage);
        }

        public bool CanShowToastMessages()
        {
            return false;
        }

        /// <summary>
        /// Show 'toast' window to notify new version is available
        /// </summary>
        /// <param name="updates">Appcast updates</param>
        /// <param name="clickHandler">handler for click</param>
        public void ShowToast(List<AppCastItem> updates, Action<List<AppCastItem>> clickHandler)
        {
        }

        /// <summary>
        /// Show message on download error
        /// </summary>
        /// <param name="message">Error message from exception</param>
        /// <param name="appcastUrl">the URL for the appcast file</param>
        public virtual void ShowDownloadErrorMessage(string message, string appcastUrl)
        {
            ShowMessage(string.Format(Resources.DefaultUIFactory_ShowDownloadErrorMessage, message));
        }

        private void ShowMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        /// <summary>
        /// Shut down the UI so we can run an update.
        /// </summary>
        public void Shutdown()
        {
            Environment.Exit(0);
        }
    }
}
