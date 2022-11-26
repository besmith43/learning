using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using McMaster.Extensions.CommandLineUtils;

namespace sadconsolegame
{
    public class Program
    {
        static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Template = "--version", ShortName = "-V")]
        bool version { get; }

        [Option(Template = "--verbose", ShortName = "-v")]
        bool verbose { get; }

        [Option(Template = "--height", ShortName = "-H")]
        static int HeightFlag { get; }

        [Option(Template = "--width", ShortName = "-W")]
        static int WidthFlag { get; }

        private void OnExecute()
        {
            if (version)
            {
                System.Console.WriteLine("Sad Console Application Version 1.0");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

            int Width = 80;
            int Height = 25;

            if (WidthFlag != 0)
            {
                Width = WidthFlag; 
            }

            if (HeightFlag != 0)
            {
                Height = HeightFlag;
            }

            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // hooks in a custom update function
            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            Console console;

            if (Program.WidthFlag != 0 && Program.HeightFlag != 0)
            {
                console = new Console(Program.WidthFlag, Program.HeightFlag);
            }
            else
            {
                console = new Console(80, 25);
            }

            console.FillWithRandomGarbage();
            console.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, 0);
            console.Print(4, 4, "Hello from SadConsole");

            SadConsole.Global.CurrentScreen = console;
        }

        private static void Update(GameTime time)
        {
            // Called each logic update.

            // As an example, we'll use the F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
        }
    }
}
