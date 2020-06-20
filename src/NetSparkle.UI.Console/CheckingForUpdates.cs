using NetSparkleUpdater.Interfaces;
using System;

namespace NetSparkleUpdater.UI.Console
{
    public class CheckingForUpdates : ICheckingForUpdates
    {
        public event EventHandler UpdatesUIClosing;

        public void Close()
        {
        }

        public void Show()
        {
            System.Console.WriteLine("Checking for updates...");
        }
    }
}
