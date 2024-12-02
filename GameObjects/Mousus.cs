using System.Numerics;
using System.Xml;
using motoret;
using Raylib_cs;

namespace exempleClasses;

public class Mousus : IGameObject, IPhysicGameObject
{
    public Vector2 _Position;
    float _Radius;
    Texture2D _TextureNoHit;
    Texture2D _TextureHit;
    Texture2D _TextureCurrent;
    Rectangle _DrawRectangleInfo;
    Rectangle _DrawRectangleCurrent;
    Vector2 _DrawOffset;
    float _Speed;
    float _SpeedTurbo = 5;
    Color _Color;
    private float _rotation;
    Vector2 _direction;
    private Vector2 _direccion1;
    //private float tiempoMuestra = 0.25f;
    //private float tiempoTranscurrido=0;
    //private List<Vector2> _recorridoDebug;
    
    

    enum CharacterStates
    {
        Idel,
        Turbo
    }
    CharacterStates currentState;
    private bool _turboActivated = false;

    public Mousus()
    {
        _Position = new Vector2 {X = Raylib.GetScreenWidth()/2, Y = Raylib.GetScreenHeight()/2 };

        InitValues();
    }

    //XML
    public Mousus(XmlNode node)
    {
        _Position.X = float.Parse(node.Attributes["x"].Value);
        _Position.Y = float.Parse(node.Attributes["y"].Value);

        InitValues();
    }

    private void InitValues()
    {
        _Radius   = 50;
        
        

        //textures
        _TextureNoHit   = Raylib.LoadTexture("assets/ship.png");
        _TextureHit     = Raylib.LoadTexture("assets/chonkus.png");
        _TextureCurrent = _TextureNoHit;

        //info de dibuix
        _DrawRectangleInfo      = new Rectangle {X = 0, Y = 0, Width = _TextureNoHit.Width, Height = _TextureNoHit.Height};
        _DrawRectangleCurrent   = new Rectangle {X = _Position.X, Y = _Position.Y, Height = 100, Width = 100};
        _DrawOffset = new Vector2 {X = 50, Y = 50};
        
        //_recorridoDebug = new List<Vector2>();
        ChangeState(CharacterStates.Idel);
    }

    public void Start() { }

    public void Update()
    {
        float deltaTime = Raylib.GetFrameTime();

        //Movement
        //Float ( Angulo y velocidad ) Coredenadas polares
        //Vidas de meteoritos
        
        if(Raylib.IsKeyDown(KeyboardKey.S))
            _Position.Y += _Speed*deltaTime;
        
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
           // _direction.Y += _rotation*deltaTime;
           _Position.Y += _Speed * (float)Math.Sin((Math.PI/180)*(_rotation-90))*deltaTime;
           _Position.X += _Speed * (float)Math.Cos((Math.PI/180)*(_rotation-90))*deltaTime;

        }
        //como lo hago sin cambiar el sprite
        //me refiero al angulo erroneo que tiene ahora mismo
        
        if(Raylib.IsKeyDown(KeyboardKey.D))
            _rotation += 30 * deltaTime;
        if(Raylib.IsKeyDown(KeyboardKey.A))
            _rotation -= 30 * deltaTime;
        
        if (Raylib.IsKeyPressed(KeyboardKey.T))
        {
            ChangeState(CharacterStates.Turbo);
        }
           
        
        Motoret.Instance.CameraPosition(_Position);

        _TextureCurrent = _TextureNoHit;
        
        UpdateState();
        /*
        tiempoTranscurrido += deltaTime;
        if (tiempoTranscurrido > tiempoMuestra)
        {
            _recorridoDebug.Add(_Position);
            tiempoTranscurrido -= tiempoMuestra;
        }*/
    }


    public void Dispose()
    {
        Raylib.UnloadTexture(_TextureHit);
        Raylib.UnloadTexture(_TextureNoHit);
    }
    
    #region StateMachine

    private void ChangeState(CharacterStates newState)
    {
        ExitState();
        InitState(newState);
    }

    private void InitState(CharacterStates newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case CharacterStates.Idel:
                _Color = Color.White;
                _Speed    = 100f;
                break;
            case CharacterStates.Turbo:
                _Color = Color.Red;
                _Speed    = 100f * _SpeedTurbo;
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case CharacterStates.Idel:

                if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    Vector2 direccion = new Vector2((float)Math.Cos((Math.PI/180)*(_rotation-90)), (float)Math.Sin((Math.PI/180)*(_rotation-90)));   
                    IGameObject bala = new Bala(_Position + direccion*(_Radius+20), direccion * 30);
                    Motoret.Instance.AddGameObject(bala);
                }
                break;
            
            case CharacterStates.Turbo:
                if( !Raylib.IsKeyDown(KeyboardKey.W) && !Raylib.IsKeyDown(KeyboardKey.S) &&
                    !Raylib.IsKeyDown(KeyboardKey.A) && !Raylib.IsKeyDown(KeyboardKey.D))
                    ChangeState(CharacterStates.Idel);
                break;
        }
    }
    
    private void ExitState()
    {
        switch (currentState)
        {
            case CharacterStates.Idel:
                break;
            case CharacterStates.Turbo:
                break;
        }
    }
    #endregion

    #region Render
    public void Render()
    {
        _DrawRectangleCurrent.X = _Position.X;
        _DrawRectangleCurrent.Y = _Position.Y;

        Raylib.DrawTexturePro(_TextureCurrent, _DrawRectangleInfo, _DrawRectangleCurrent, _DrawOffset, _rotation, _Color);
        Raylib.DrawCircleLinesV(_Position, _Radius, Color.Lime);
    /*Dots Debug
        foreach (var punto in _recorridoDebug)
        {
            Raylib.DrawCircle((int)punto.X, (int)punto.Y, 5, Color.Red);
        }*/
    }

    public void RenderGUI()
    {
        Raylib.DrawText($"Position: ({_Position.X},{_Position.Y})", 12, 12, 20, Color.DarkGreen);
        //Raylib.DrawText($"Rotation: ({_rotation})", 12, 52, 20, Color.Black);
        //Raylib.DrawText($"Coseno: ({(float)Math.Cos((Math.PI/180)*_rotation)})", 12, 80, 20, Color.Black);
        //Raylib.DrawText($"Seno: ({(float)Math.Sin((Math.PI/180)*_rotation)})", 12, 100, 20, Color.Black);
        
        if(_TextureCurrent.Equals(_TextureNoHit))
            Raylib.DrawText("No col·lisionant", 12, 36, 20, Color.Black);
        else
            Raylib.DrawText("Col·lisionant", 12, 36, 20, Color.Red);
    }
    #endregion
    
    #region Physics
    //PhysicGameObject
    public void HasCollidedWith(IPhysicGameObject other)
    {
        _TextureCurrent = _TextureHit;
    }

    public bool IsCollidingWith(IPhysicGameObject other)
    {
        return other.IsCollidingWith(_Position, _Radius);
    }

    public bool IsCollidingWith(Rectangle other)
    {
        return Raylib.CheckCollisionCircleRec(_Position, _Radius, other);
    }

    public bool IsCollidingWith(Vector2 otherCenter, float otherRadius)
    {
        return Raylib.CheckCollisionCircles(_Position, _Radius, otherCenter, otherRadius);
    }
    #endregion

    //XMLexporter
    public XmlElement ToXML(XmlDocument document)
    {
        XmlElement node = document.CreateElement("mousus");
        node.SetAttribute("x", _Position.X.ToString());
        node.SetAttribute("y", _Position.Y.ToString());

        return node;
    }
}