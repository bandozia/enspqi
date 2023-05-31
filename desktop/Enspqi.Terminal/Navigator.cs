using Enspqi.Terminal.Views;
using Terminal.Gui;

namespace Enspqi.Terminal;

public class Navigator : INavigator
{
    public event NavigateDelegate? OnNavigate;

    private readonly IChatService _chatService;

    public Navigator(IChatService chatService)
    {
        _chatService = chatService;
    }

    public void Navigate<T>() where T : BaseView
    {
        var view = (T?)Activator.CreateInstance(typeof(T), new object[] { this, _chatService })
            ?? throw new NotImplementedException("invalid view");
        
        OnNavigate?.Invoke(view);
    }
}
