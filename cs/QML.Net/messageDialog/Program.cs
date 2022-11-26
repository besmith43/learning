using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

namespace messageDialog
{
    class Program
    {
        static int Main(string[] args)
        {
            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();
            
            QQuickStyle.SetStyle("Material");

            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    Qml.Net.Qml.RegisterType<DialogTestModel>("messageDialog");

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
