using NetSparkleUpdater;
using NetSparkleUpdater.SignatureVerifiers;
using System;
using System.Linq;
using System.Threading;

namespace NetSparkle.Samples.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var sparkle = new SparkleUpdater("https://netsparkleupdater.github.io/NetSparkle/files/sample-app/appcast.xml", new DSAChecker(NetSparkleUpdater.Enums.SecurityMode.Strict))
            {
                UIFactory = new NetSparkleUpdater.UI.Console.UIFactory()
            };
            
            // TLS 1.2 required by GitHub (https://developer.github.com/changes/2018-02-01-weak-crypto-removal-notice/)
            sparkle.SecurityProtocolType = System.Net.SecurityProtocolType.Tls12;

            bool updateFinished = false;
            UpdateInfo updateInfo = null;

            sparkle.UserRespondedToUpdate += (sender, e) =>
            {
                if (e.Result == NetSparkleUpdater.Enums.UpdateAvailableResult.RemindMeLater)
                {
                    updateFinished = true;
                }
            };

            sparkle.PreparingToExit += (sender, e) =>
            {
                updateFinished = true;
            };

            var checkUpdateTask = sparkle.CheckForUpdatesQuietly();
            checkUpdateTask.Wait();
            updateInfo = checkUpdateTask.Result;

            if (updateInfo.Status == NetSparkleUpdater.Enums.UpdateStatus.UpdateAvailable)
            {
                sparkle.ShowUpdateNeededUI();
                
                while (!updateFinished)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
