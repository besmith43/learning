using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

namespace net48test
{
    class Program
    {
        static int Main(string[] args)
        {
            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    #if DEBUG
                         qmlEngine.Load("pages/main.qml");
                    #else
                        qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/pages/main.qml");
                    #endif
                    
                    return application.Exec();
                }
            }
        }
    }
}
