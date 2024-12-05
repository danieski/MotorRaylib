using System.Numerics;
using System.Xml;
using motoret;
using Raylib_cs;


namespace exempleClasses;

public class Personaje : IGameObject, IPhysicGameObject
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
    private float _radarAngle = 0f;
    private float _radarRadius = 300f;
    private float _radarSpeed = 1f;
    private Vector2 _radarPosition;
    private Sound deathSound;
    private Sound shootSound;
    private bool _deathSoundPlayed = false; 
    private float _tiempoReinicio = 6f; // Tiempo en segundos para reiniciar
    private float _tiempoTranscurrido = 0f; 
    private Music music;
    enum CharacterStates
    {
        Idel,
        Turbo,
        Death
    }
    CharacterStates currentState;
    private bool _turboActivated = false;

    public Personaje()
    {
        _Position = new Vector2 {X = Raylib.GetScreenWidth()/2, Y = Raylib.GetScreenHeight()/2 };

        InitValues();
    }

    //XML
    public Personaje(XmlNode node)
    {
        _Position.X = float.Parse(node.Attributes["x"].Value);
        _Position.Y = float.Parse(node.Attributes["y"].Value);

        InitValues();
    }

    private void InitValues()
    {
    
    
        _Radius   = 25;
        //Audio
        Raylib.InitAudioDevice(); 
        deathSound = Raylib.LoadSound("assets/death.wav");
        shootSound = Raylib.LoadSound("assets/shoot.wav");
        music = Raylib.LoadMusicStream("assets/Sonar.wav");
        Raylib.PlayMusicStream(music);

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
        Raylib.UpdateMusicStream(music);
        float deltaTime = Raylib.GetFrameTime();

        //Movement
        //Float ( Angulo y velocidad ) Coredenadas polares
        //Vidas de meteoritos
        if (currentState == CharacterStates.Death && !_deathSoundPlayed) 
        {
            Raylib.PlaySound(deathSound);
            Console.WriteLine("Reproduciendo sonido");
            _deathSoundPlayed = true; 
            
        }
        
        if(Raylib.IsKeyDown(KeyboardKey.S) && currentState != CharacterStates.Death)
            _Position.Y += _Speed*deltaTime;
        
        if (Raylib.IsKeyDown(KeyboardKey.W)&& currentState != CharacterStates.Death)
        {
           // _direction.Y += _rotation*deltaTime;
           _Position.Y += _Speed * (float)Math.Sin((Math.PI/180)*(_rotation-90))*deltaTime;
           _Position.X += _Speed * (float)Math.Cos((Math.PI/180)*(_rotation-90))*deltaTime;

        }
        //como lo hago sin cambiar el sprite
        //me refiero al angulo erroneo que tiene ahora mismo
        
        if(Raylib.IsKeyDown(KeyboardKey.D)&& currentState != CharacterStates.Death)
            _rotation += 30 * deltaTime;
        if(Raylib.IsKeyDown(KeyboardKey.A)&& currentState != CharacterStates.Death)
            _rotation -= 30 * deltaTime;
        
        if (Raylib.IsKeyPressed(KeyboardKey.T))
        {
            ChangeState(CharacterStates.Turbo);
        }
           
        
        Motoret.Instance.CameraPosition(_Position);

        _TextureCurrent = _TextureNoHit;
        
        UpdateState();
        
        _radarAngle += _radarSpeed * deltaTime;
        if (_radarAngle > MathF.PI *2)
        {
            _radarAngle -= MathF.PI *2;
        }

        if (_deathSoundPlayed) // Si el personaje ha muerto
        {
            _tiempoTranscurrido += Raylib.GetFrameTime(); // Actualizar el contador

            if (_tiempoTranscurrido >= _tiempoReinicio)
            {
                Motoret.Instance.Stop();
            }
        }
    }


    public void Dispose()
    {
        Raylib.UnloadTexture(_TextureHit);
        Raylib.UnloadTexture(_TextureNoHit);
        Raylib.UnloadSound(deathSound); 
        Raylib.UnloadSound(shootSound);// Descargar el sonido
        Raylib.UnloadMusicStream(music);// Descargar el sonido
        Raylib.CloseAudioDevice();
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
            case CharacterStates.Death:
                
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
                    Raylib.PlaySound(shootSound);
                    Console.WriteLine("Reproduciendo sonido shoot");
                }
                break;
            
            case CharacterStates.Turbo:
                if( !Raylib.IsKeyDown(KeyboardKey.W) && !Raylib.IsKeyDown(KeyboardKey.S) &&
                    !Raylib.IsKeyDown(KeyboardKey.A) && !Raylib.IsKeyDown(KeyboardKey.D))
                    ChangeState(CharacterStates.Idel);
                break;
            
            case CharacterStates.Death:
                
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
        _radarPosition.X = _Position.X+(_radarRadius+100)*(float)MathF.Cos(_radarAngle);
        _radarPosition.Y = _Position.Y+(_radarRadius+100)*(float)MathF.Sin(_radarAngle);
        //Vector2 direccion = new Vector2((float)Math.Cos((Math.PI/180)*(_rotation-90)), (float)Math.Sin((Math.PI/180)*(_rotation-90)));   

        
        Raylib.DrawLineV(_Position,_radarPosition,Color.Lime);
       // Raylib.DrawLine((int)_Position.X, (int)_Position.Y, _radarStickX, _radarStickY,Color.DarkGreen);
        //Vector2 direccion = new Vector2((float)Math.Cos((Math.PI/180)*(_rotation-90)), (float)Math.Sin((Math.PI/180)*(_rotation-90)));
    }

    public void RenderGUI()
    {
        Raylib.DrawText($"Position: ({_Position.X},{_Position.Y})", 12, 12, 20, Color.DarkGreen);

        
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
        //Console.WriteLine("Collision %d" + other.ToString());
        if (other.IsCollidingWith(_Position, _Radius))
        {
            //Console.WriteLine("Collision with my Circle");
            //Motoret.Instance.Stop();
            ChangeState(CharacterStates.Death);
        }
    }

    public bool IsCollidingWith(IPhysicGameObject other)
    {
        return other.IsCollidingWith(_Position, _Radius) || other.IsCollidingWith(_Position, _radarPosition, 10f);
    }

    public bool IsCollidingWith(Rectangle other)
    {
        return Raylib.CheckCollisionPointLine(new Vector2(other.X,other.Y),_Position, _radarPosition,10);
    }

    public bool IsCollidingWith(Vector2 otherCenter, float otherRadius)
    {
        return Raylib.CheckCollisionPointLine(otherCenter,_Position, _radarPosition,100);
    }
    public bool IsCollidingWith(Vector2 lineStart, Vector2 lineEnd, float lineThickness)
    {
        return false;
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