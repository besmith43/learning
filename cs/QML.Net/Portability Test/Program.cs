using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using Qml.Net;
using Qml.Net.Runtimes;

namespace Portability_Test
{
    class Program
    {
        static int Main(string[] args)
        {
            if (!TestVCRuntime())
            {
                InstallVCRuntime();
            }

            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/pages/main.qml");
                    
                    return application.Exec();
                }
            }
        }

        public static bool TestVCRuntime()
        {
            return File.Exists(@"C:\Windows\System32\msvcp140.dll");
        }

        public static void InstallVCRuntime()
        {
            string installer = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/VC_redist.x64.exe";

            if (!File.Exists(installer))
            {
                DownloadVCRuntime();
            }

            if (File.Exists(installer))
            {
                var installerProcess = Process.Start(installer, "/install /quiet /noreboot");

                installerProcess.Start();
                installerProcess.WaitForExit();
            }
        }

        public static void DownloadVCRuntime()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://aka.ms/vs/16/release/vc_redist.x64.exe", $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/vc_redist.x64.exe");
            }
        }
    }
}
