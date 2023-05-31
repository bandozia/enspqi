using Enspqi.Terminal;
using Terminal.Gui;

Application.Init();

try
{    
    Application.Run(new MainView());
}
finally
{
    Application.Shutdown();
}
