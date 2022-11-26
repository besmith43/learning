using System;
using System.IO;
using System.Collections;
using Qml.Net;
using Qml.Net.Runtimes;

namespace Hello_World
{
    class Program
    {
        static int Main(string[] args)
        {
            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

            foreach(DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine($"{ item.Key }:{ item.Value }");
            }

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
