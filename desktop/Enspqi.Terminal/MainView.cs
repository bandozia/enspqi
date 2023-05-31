using Enspqi.Terminal.Views;
using Terminal.Gui;

namespace Enspqi.Terminal;

public class MainView : Window
{
    private readonly IChatService _chatService;
    private readonly INavigator _navigator;

    public MainView()
    {        
        _chatService = new ChatService();
        _navigator = new Navigator(_chatService);

        Title = "Enspqi (ctr+Q para fechar)";
        Border.BorderStyle = BorderStyle.Rounded;

        ColorScheme = new ColorScheme
        {
            Normal = Application.Driver.MakeAttribute(Color.Gray, Color.DarkGray)
        };
                
        _navigator.OnNavigate += Navigated;

        _navigator.Navigate<HubConnView>();                
    }

    private void Navigated(BaseView view)
    {        
        RemoveAll();

        GetCurrentWidth(out var w);
        GetCurrentHeight(out var h);

        view.Width = w > 0 ? w : Dim.Fill(0);
        view.Height = h > 0 ? h : Dim.Fill(0);

        Add(view);
    }
}
