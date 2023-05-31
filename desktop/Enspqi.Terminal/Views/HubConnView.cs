using Terminal.Gui;

namespace Enspqi.Terminal.Views;

public class HubConnView : BaseView
{
    private TextField? _userNameInput;

    public HubConnView(INavigator navigator, IChatService chatService) : base(navigator, chatService)
    {        
    }      

    public override void Build()
    {
        var bt = new Button("conectar")
        {
            X = Pos.Center(),
            Y = Pos.Center() + 2,
        };

        _userNameInput = new TextField()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = 30
        };

        bt.Clicked += ConnectClicked;

        Add(
            new Label("Escolha um nome e conecte-se ao hub")
            {
                X = Pos.Center(),
                Y = Pos.Center() - 3
            },
           _userNameInput,
           bt
        );   
    }

    private void ConnectClicked()
    {
        if (_userNameInput!.Text.Length < 3)
        {
            MessageBox.Query("Nome", "Escolhe um nome maior", "tá");
            return;
        }

        RemoveAll();

        Add(new Label("conectando...")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        });

        try
        {
            Task.Run(async () =>
            {
                await ChatService.Connect($"http://localhost:5104/chat/general?displayName={_userNameInput.Text}");
                Dispose();
                Navigator.Navigate<HubView>();
            });
        }
        catch (Exception ex)
        {
            MessageBox.ErrorQuery("Erro", $"Falha na conexão. {ex.InnerException?.Message ?? ex.Message}", "ok");
            RemoveAll();
            Build();
        }
    }

}
