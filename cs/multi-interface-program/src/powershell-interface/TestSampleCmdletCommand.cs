using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using doWorkLib;

namespace powershell_interface
{
    [Cmdlet(VerbsDiagnostic.Test, "SampleCmdlet")]
    [OutputType(typeof(int))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        [ValidateSet("add", "sub", "mul", "div")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true)]
        public string operation { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true)]
        public int a { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipeline = true)]
        public int b { get; set; }

        public int c { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            switch(operation)
			{
                case "add":
                    c = SharedClass.add(a, b);
                    break;
                case "sub":
                    c = SharedClass.sub(a, b);
                    break;
                case "mul":
                    c = SharedClass.mul(a, b);
                    break;
                case "div":
                    c = SharedClass.div(a, b);
                    break;
			}
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            this.WriteObject(c);
            WriteVerbose("End!");
        }
    }
}
