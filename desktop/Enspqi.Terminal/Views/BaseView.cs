using Terminal.Gui;

namespace Enspqi.Terminal.Views;

public abstract class BaseView : View
{
    protected readonly INavigator Navigator;
    protected readonly IChatService ChatService;

    public BaseView(INavigator navigator, IChatService chatService)
    {
        Navigator = navigator;
        ChatService = chatService;

        Build();
    }

    public abstract void Build();
}
