// some action function isn't outputting to console because the menu isn't closing when the action is called like I expected

using System.Diagnostics.Tracing;
using ConsoleTools;

var menu = new ConsoleMenu(args, level: 0)
  .Add("One", () => SomeAction("One"))
  .Add("Two", () => SomeAction("Two"))
  .Add("Three", () => SomeAction("Three"))
  .Add("Change me", (thisMenu) => thisMenu.CurrentItem.Name = "I am changed!")
  .Add("Close", ConsoleMenu.Close)
  .Add("Action then Close", (thisMenu) => { SomeAction("Close"); thisMenu.CloseMenu(); })
  .Add("Exit", () => Environment.Exit(0))
  .Configure(config =>
  {
      config.Selector = "--> ";
      config.EnableFilter = true;
      config.Title = "Main menu";
      config.EnableWriteTitle = true;
      config.EnableBreadcrumb = true;
  });

menu.Show();


void SomeAction(string name)
{
    Console.WriteLine($"Action '{name}'");
}
