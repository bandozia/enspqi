using Terminal.Gui;

namespace Enspqi.Terminal.Views;

public class HubView : BaseView
{
    private readonly List<string> _rooms = new List<string>();
    private ListView? _list;
    private TextField? _newRoomInput;

    public HubView(INavigator navigator, IChatService chatService) : base(navigator, chatService)
    {
        ChatService.OnConnectionDown += ChatService_OnConnectionDown;
        ChatService.OnNewRoomCreated += NewRoomCreatedByOther;
        // TODO: query for rooms
    }
       
    private void ChatService_OnConnectionDown()
    {
        RemoveAll();
        Add(new Label("A conexão com o servidor caiu e eu ainda não escrevi nada para lidar com isso. Mea culpa")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        });
    }

    public override void Build()
    {
        _list = new ListView(_rooms)
        {
            Width = Dim.Percent(35),
            Height = Dim.Fill(3) - 3,
        };
                               
        var roomsPanel = new PanelView()
        {
            Border = new Border
            {
                BorderStyle = BorderStyle.Rounded,
                DrawMarginFrame = true,
                Title = "salas"                
            }
        };

        var connectBt = new Button("Entrar")
        {
            X = 1,
            Y = Pos.Bottom(roomsPanel),
            Width = Dim.Percent(35),
        };

        var createRoomPanel = new FrameView()
        {
            X = Pos.Right(roomsPanel),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Title = "criar sala",
            Border = Border = new Border
            {
                BorderStyle = BorderStyle.Rounded,
                DrawMarginFrame = true,
                Title = "criar nova sala",
            }
        };

        _newRoomInput = new TextField()
        {
            Width = Dim.Fill() - 15,
            X = Pos.Center(),
            Y = Pos.Center() - 2
        };

        var createRoomBt = new Button("criar")
        {
            Width = 30,
            Y = Pos.Center() + 2,
            X = Pos.Center()
        };

        connectBt.Clicked += JoinRoom;
        createRoomBt.Clicked += CreateRoomAndJoin;

        roomsPanel.Add(_list);
        createRoomPanel.Add(_newRoomInput, createRoomBt);
        Add(roomsPanel, connectBt, createRoomPanel);
    }

    private void CreateRoomAndJoin()
    {
        if (_newRoomInput!.Text.Length < 3)
        {
            MessageBox.Query("Nome", "escolhe um nome maior...", "tá");
            return;
        }

        Task.Run(async () =>
        {           
            var room = await ChatService.CreateRoomAndJoin($"{_newRoomInput.Text}");
            
            if (room != null)
            {
                //TODO: navigate to chat room
            }
        });
    }

    private void JoinRoom()
    {
        //Console.WriteLine(_rooms[_list!.SelectedItem]);
    }

    private void NewRoomCreatedByOther(Room room)
    {
        MessageBox.Query("room", $"alguem criou uma sala {room.Name}", "ok");
    }
}
