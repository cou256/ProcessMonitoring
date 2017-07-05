using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProcessMonitoring
{
    using CAUtility;
    /// <summary>
    /// プロセス監視
    /// </summary>
    public class MainMonitoring
    {
        const String Xml = "ProcessMonitoringSetting.xml";
        public MainMonitoring()
        {
            var setting = XmlSetting.Load<Setting>(Xml);
            Run(setting.FileName, setting.Options, setting.MonitoringInterval);
        }
        void Run(String fileName, String arguments, int monitoringInterval)
        {
            var process = Process.Start(fileName, arguments);
            process.EnableRaisingEvents = true;
            process.Exited += (e, a) => Run(fileName, arguments, monitoringInterval);
            while (true)
            {
                SwitchToThisWindow(process.MainWindowHandle, true);
                Thread.Sleep(monitoringInterval);
            }
        }
        static void Main()
        {
            new MainMonitoring();
        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        public class Setting
        {
            public String FileName;
            public String Options;
            public int MonitoringInterval;
        }
    }
}