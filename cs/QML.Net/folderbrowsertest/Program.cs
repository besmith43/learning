using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

namespace folderbrowsertest
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
                    Qml.Net.Qml.RegisterType<FileTestModel>("Testing");

                    qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/pages/main.qml");
                    
                    return application.Exec();
                }
            }
        }
    }
}
