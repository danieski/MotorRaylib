using System.Numerics;
using System.Xml;
using motoret;
using Raylib_cs;

namespace exempleClasses;

public class ExitButton : IGameObject
{
    Rectangle _DrawRectangle;
    Color _DrawColor;
    Color _MenuColor = Color.Green;
    Rectangle _MenuRectangle = new Rectangle(new Vector2(320, 30), new Vector2(150, 20));
    enum GameState
    {
        Menu,
        Game,
        Pause
    }

    private GameState currentState;
    public void Start()
    {
        initState();
        _DrawRectangle = new Rectangle
        {
            X = Raylib.GetScreenWidth() - 60,
            Y = 40,
            Width = 20,
            Height = 20
        };
    }

    public void Update()
    {
        if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), _DrawRectangle))
        {
            if(Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                XMLExporter.SaveFile("savegame.xml", Motoret.Instance.GetGameObjects());
                Motoret.Instance.Stop();
            }
            
            _DrawColor = Color.Red;
        }else{
            _DrawColor = Color.Blue;
        }

        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), _MenuRectangle))
        {
            _MenuColor = Color.Green;
            
            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
                currentState = GameState.Game;
        }
        updateState();
    }

    public void Dispose() { }
    
    #region StateMachine

    private void changeState(GameState newState)
    {
        currentState = newState;
    }
    private void updateState()
    {
        
    }
    private void initState()
    {
        currentState = GameState.Menu;
    }
    private void exitState()
    {
        
    }
   
    #endregion
    
    public void Render() { }

    public void RenderGUI()
    {
        Raylib.DrawRectangleRec(_MenuRectangle, Color.Orange);
        switch (currentState)
        {
            case GameState.Menu:
                    
                Raylib.DrawText("Menu State", 340, 30, 20,Color.Red);
                    
                break;
            
            case GameState.Game:
                
                Raylib.DrawText("Game State", 340, 30, 20, Color.Green);
                
                break;
                
        }
        
        Raylib.DrawRectangleRec(_DrawRectangle, _DrawColor);
    }

    public XmlElement ToXML(XmlDocument document)
    {
        XmlElement node = document.CreateElement("exitbutton");

        return node;
    }
}