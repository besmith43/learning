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
using System.Runtime.InteropServices;  // needed for console.writeline

namespace HelloWorld
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
            AttachConsole( ATTACH_PARENT_PROCESS ); // needed for console.writeline

            InitializeComponent();

            Console.WriteLine("Starting Main Window");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button was Clicked");
        }
    }
}
