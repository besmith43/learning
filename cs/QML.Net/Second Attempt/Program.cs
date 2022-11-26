using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

namespace Second_Attempt
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
                         qmlEngine.Load("qml/main.qml");
                    #else
                        qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/qml/main.qml");
                    #endif
                    
                    return application.Exec();
                }
            }
        }
    }
}
