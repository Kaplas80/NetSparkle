using System;
using System.Collections.Generic;
using System.Linq;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Events;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUpdater.UI.Console
{
    public class UpdateAvailable : IUpdateAvailable
    {
        private readonly SparkleUpdater _sparkle;
        private readonly List<AppCastItem> _updates;

        /// <summary>
        /// Event fired when the user has responded to the 
        /// skip, later, install question.
        /// </summary>
        public event UserRespondedToUpdate UserResponded;

        /// <summary>
        /// The result of ShowDialog()
        /// </summary>
        public UpdateAvailableResult Result { get; private set; }

        /// <summary>
        /// The current item being installed
        /// </summary>
        public AppCastItem CurrentItem => _updates.Count() > 0 ? _updates[0] : null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sparkle">The <see cref="SparkleUpdater"/> instance to use</param>
        /// <param name="items">List of updates to show. Should contain at least one item.</param>
        public UpdateAvailable(SparkleUpdater sparkle, List<AppCastItem> items)
        {
            _sparkle = sparkle;
            _updates = items;
            this.Result = UpdateAvailableResult.RemindMeLater;
        }

        public void Show(bool IsOnMainThread)
        {
            AppCastItem item = _updates.FirstOrDefault();

            if (item != null)
            {
                var versionString = "";
                try
                {
                    // Use try/catch since Version constructor can throw an exception and we don't want to
                    // die just because the user has a malformed version string
                    Version versionObj = new Version(item.AppVersionInstalled);
                    versionString = NetSparkleUpdater.Utilities.GetVersionString(versionObj);
                }
                catch
                {
                    versionString = "?";
                }

                System.Console.Write(string.Format("{0} {2} is now available (you have {1}). Would you like to install it now? (Y/n) ", item.AppName, versionString, item.Version));
            }
            else
            {
                // TODO: string translations (even though I guess this window should never be called with 0 app cast items...)
                System.Console.Write("Would you like to install it now? (Y/n)");
            }

            var result = System.Console.ReadLine();
            if (result.ToLowerInvariant() != "n")
            {
                this.Result = UpdateAvailableResult.InstallUpdate;
            }
            else
            {
                this.Result = UpdateAvailableResult.RemindMeLater;
            }

            this.SendResponse(this.Result);
        }

        public void HideReleaseNotes()
        {
        }

        public void HideRemindMeLaterButton()
        {
        }

        public void HideSkipButton()
        {
        }

        public void BringToFront()
        {
        }

        public void Close()
        {
        }

        private void SendResponse(UpdateAvailableResult response)
        {
            UserResponded?.Invoke(this, new UpdateResponseEventArgs(response, this.CurrentItem));
        }
    }
}
