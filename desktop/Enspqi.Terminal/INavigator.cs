using Enspqi.Terminal.Views;

namespace Enspqi.Terminal;

public delegate void NavigateDelegate(BaseView view);

public interface INavigator
{    
    event NavigateDelegate? OnNavigate;

    void Navigate<T>() where T : BaseView;
}
