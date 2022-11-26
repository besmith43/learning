using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using CommandLine;

namespace CMDArgTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport( "kernel32.dll" )]   // needed for console.writeline
        static extern bool AttachConsole( int dwProcessId );
        private const int ATTACH_PARENT_PROCESS = -1;

        public MainWindow()
        {
            AttachConsole( ATTACH_PARENT_PROCESS );
            
            String[] args = App.Args;

            Console.WriteLine("Starting Main Window");

            try  // the try/catch is necessary so that you can handle when there isn't any arguments given
            {
                var parsedArgs = Parser.Default.ParseArguments<Options>(args)
                    .WithParsed(Run)
                    .WithNotParsed(HandleParseError);
            }
            catch
            {
                Console.WriteLine("There are no arguments given");
            }

            InitializeComponent();
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {
                Console.WriteLine("Version Request");
                return;
            }

            if (errs.IsHelp())
            {
                Console.WriteLine("Help Request");
                return;
            }
            Console.WriteLine("Parser Fail");
        }

        private static void Run(Options opts)
        {
            if (opts.Verbose)
            {
                Console.WriteLine("Verbose is set to true");
            }
        }
    }

    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
}
