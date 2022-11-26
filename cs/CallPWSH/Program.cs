using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace CallPWSH
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hosted = new PWSH();
            await hosted.RunScript();
        }
    }

    public class PWSH
    {
        public async Task RunScript()
        {
            string scriptDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var script = 
                            "Start-Sleep -Milliseconds 500" + Environment.NewLine +
                            "write-error \"testing\"" + Environment.NewLine +
                            "write-Information -MessageData \"Progress: 25%\"" + Environment.NewLine +
                            "Start-Sleep -Milliseconds 500" + Environment.NewLine +
                            "write-error \"testing\"" + Environment.NewLine +
                            "write-Information -MessageData \"Progress: 50%\"" + Environment.NewLine +
                            "Start-Sleep -Milliseconds 500" + Environment.NewLine +
                            "write-error \"testing\"" + Environment.NewLine +
                            "write-Information -MessageData \"Progress: 75%\"" + Environment.NewLine +
                            "Start-Sleep -Milliseconds 500" + Environment.NewLine +
                            "write-error \"testing\"" + Environment.NewLine +
                            "write-Information -MessageData \"Progress: 100%\"" + Environment.NewLine +
                            "Start-Sleep -Milliseconds 500";

            var defaultSessionState = InitialSessionState.CreateDefault();
            defaultSessionState.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;

            var RsPool = RunspaceFactory.CreateRunspacePool(defaultSessionState);
            RsPool.SetMinRunspaces(2);
            RsPool.SetMaxRunspaces(10);

            RsPool.ThreadOptions = PSThreadOptions.UseNewThread;

            RsPool.Open();

            using (PowerShell ps = PowerShell.Create())
            {
                ps.RunspacePool = RsPool;

                ps.AddScript(script);

                ps.Streams.Error.DataAdded += Error_DataAdded;
                ps.Streams.Information.DataAdded += Information_DataAdded;

                var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);
            }
        }

        private void Information_DataAdded(object sender, DataAddedEventArgs e)
        {
          var streamObjectsReceived = sender as PSDataCollection<InformationRecord>;
          var currentStreamRecord = streamObjectsReceived[e.Index];

          Console.WriteLine($"InfoStreamEvent: {currentStreamRecord.MessageData}");
        }

        private void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            var streamObjectsReceived = sender as PSDataCollection<ErrorRecord>;
            var currentStreamRecord = streamObjectsReceived[e.Index];

            Console.WriteLine($"ErrorStreamEvent: {currentStreamRecord.Exception}");
        }
    }
}
