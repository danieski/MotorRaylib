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
    private int points;
    enum GameState
    {
        Menu,
        Game,
        Pause
    }

    private GameState currentState;
    public void Start()
    {
        _DrawRectangle = new Rectangle
        {
            X = Raylib.GetScreenWidth() - 50,
            Y = 30,
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
            _DrawColor = Color.DarkGreen;
        }

        
       // updateState();
        points = Motoret.Instance.ShowPoints();
    }

    public void Dispose() { }
    
    #region StateMachine

    private void changeState(GameState newState)
    {
        currentState = newState;
        Console.WriteLine("changestate");
    }
    private void updateState()
    {
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), _MenuRectangle))
        {
            _MenuColor = Color.DarkGreen;

            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {

                changeState(GameState.Pause);
            }
        }

        while (currentState == GameState.Pause)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            // Dibujamos el menú de pausa
            Raylib.DrawRectangleRec(_MenuRectangle, _MenuColor);
            Raylib.DrawText("Resume", (int)_MenuRectangle.X + 10, (int)_MenuRectangle.Y + 10, 20, Color.White);

            // Verificamos si el ratón está sobre el rectángulo del botón
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), _MenuRectangle))
            {
                Console.WriteLine("Mouse over button");
                _MenuColor = Color.DarkGreen; // Cambiamos el color al pasar el ratón

                if (Raylib.IsMouseButtonReleased(MouseButton.Left))
                {
                    currentState = GameState.Game; // Cambiamos el estado al juego
                    Console.WriteLine("Mouse clicked, resuming game");
                }
            }
            else
            {
                _MenuColor = Color.DarkGray; // Color predeterminado
            }

            Raylib.EndDrawing();
        }
    }


   
    #endregion
    
    public void Render() { }

    public void RenderGUI()
    {
        Raylib.DrawRectangleRec(_MenuRectangle, Color.DarkGray);
        switch (currentState)
        {
            case GameState.Menu:
                    
                Raylib.DrawText("Pause Menu", 340, 30, 20,Color.Green);
                    
                break;
            
            case GameState.Game:
                
                Raylib.DrawText("Reanude Game", 340, 30, 20, Color.Green);
                
                break;
                
        }
        
        Raylib.DrawRectangleRec(_DrawRectangle, _DrawColor);
        Raylib.DrawText($"Points: {points}",12,34,20,Color.DarkGreen);
    }

    public XmlElement ToXML(XmlDocument document)
    {
        XmlElement node = document.CreateElement("exitbutton");

        return node;
    }
    private void ChangeState(GameState newState)
    {
        ExitState();

        InitState(newState);
        
    }
    private void ExitState()
    {
        switch (currentState)
        {
            case GameState.Game:
                break;
            case GameState.Menu:
                break;   
            case GameState.Pause:
                break;
        }
    }

    private void InitState(GameState newState)
    {
    }

   

}