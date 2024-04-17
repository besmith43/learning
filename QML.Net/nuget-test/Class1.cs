using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

namespace nuget_test
{
    public class QmlFormTest
    {
        public string Start()
        {
            RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

            QQuickStyle.SetStyle("Material");

            using (var application = new QGuiApplication())
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    Qml.Net.Qml.RegisterType<FileModel>("Test");

                    #if DEBUG
                         qmlEngine.Load("qml/main.qml");
                    #else
                        qmlEngine.Load($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/qml/main.qml");
                    #endif
                    
                    int runtimeExitCode = application.Exec();
                }
            }
        }
    }

    public class FileModel
    {
        private string filename = "";

        public void SetFile(string chosenFilename)
        {
            filename = chosenFilename;
            Console.WriteLine(filename);
        }

        public string GetFile()
        {
            return filename;
        }
    }
}
