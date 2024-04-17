using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;
using McMaster.Extensions.CommandLineUtils;

namespace CmdLineParsingTest
{
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "Version", ShortName = "V")]
        public bool Version { get; }

        public static string VersionNumber = "1.0";

        private int OnExecute()
        {
            if (Version)
            {
                GetVersion();
                return 0;
            }

            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();
            
            QQuickStyle.SetStyle("Material");

            using (var application = new QGuiApplication())
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    #if DEBUG
                         qmlEngine.Load("qml/main.qml");
                    #else
                        qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/qml/main.qml");
                    #endif
                    
                    return application.Exec();
                }
            }
        }

        public static void GetVersion()
        {
            Console.WriteLine($"CmdLineParsingTest Version: { VersionNumber }");
        }
    }
}
