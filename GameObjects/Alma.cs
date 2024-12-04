using System.Numerics;
using System.Xml;
using motoret;
using Raylib_cs;

namespace exempleClasses;

public class Alma : IGameObject, IPhysicGameObject
{
    Rectangle _Position;
    Color _ColorCurrent;
    Color _ColorNoHit;
    Color _ColorHit;
    Texture2D _Texture;
    Rectangle _DrawRectangleInfo;
    private LifeBar _currentLifeBar;
    private Vector2 _RadarPoint;
    private bool _showAlma = false;
    
    private float _AlmaTimeMaxShow = 5;
    private float _AlmaTimeCounter = 0;

    enum LifeBar 
    {
        zero,
        one,
        two,
        three,
        four
    }

    public Alma(Texture2D texture, int width, int height)
    {
        _Position.X = 0;
        _Position.Y = 0;

        InitValues(texture);
    }

    public Alma(XmlNode node)
    {
        _Position.X = float.Parse(node.Attributes["x"].Value);
        _Position.Y = float.Parse(node.Attributes["y"].Value);

        //Això no ho feu, és per no implementar-vos a l'exemple la part
        //de gestor d'assets del motor.
        InitValues(ExempleClasses.TexturaAlma);
    }

    private void InitValues(Texture2D texture)
    {
        _Position.Height = 10;
        _Position.Width = 10;

        _Texture = texture;

        _ColorHit   = Color.Red;
        _ColorNoHit = Color.White;
        _ColorCurrent = _ColorNoHit;

        //info de dibuix
        _DrawRectangleInfo = new Rectangle {X = 0, Y = 0, Width = _Texture.Width, Height = _Texture.Height};
        _RadarPoint = new Vector2(_Position.X, _Position.Y);
    }

    public void Start() { }

    public void Update()
    {
        if (_showAlma)
        {
            _AlmaTimeCounter =+ 1 * Raylib.GetFrameTime();
            if (_AlmaTimeCounter > _AlmaTimeMaxShow)
            {
                _AlmaTimeCounter = 0;
                _showAlma = false;
            }
            
        }
    }

    //Al ser textura compartida, no l'alliberem
    public void Dispose() { }

    public void Render()
    {
   
        switch (_currentLifeBar)
        {
            case LifeBar.zero:
                _ColorCurrent = Color.Green;
                break;
            case LifeBar.one:
                _ColorCurrent = Color.DarkGreen;
                break;
            case LifeBar.two:
                break;
            case LifeBar.three:
                break;
            case LifeBar.four:
                motoret.Motoret.Instance.RemoveGameObject(this);
                break;
            
        }
        if (_showAlma)
        {
            Raylib.DrawRectangleLinesEx(_Position, 5, _ColorCurrent);
            _showAlma = false;
        }

        
    }

    public void RenderGUI() { }

    //PhysicGameObject
    public void HasCollidedWith(IPhysicGameObject other)
    {
 
        if (other is Bala)
        {
            _currentLifeBar += 1;
            Console.WriteLine("Choco desde alma (Bala)");
        }

        if (other is Personaje)
        {
            _showAlma = true;
        }
        Console.WriteLine("Choco desde alma" + other );
    }

    public bool IsCollidingWith(IPhysicGameObject other)
    {
        return other.IsCollidingWith(_Position);
    }

    public bool IsCollidingWith(Rectangle other)
    {
        return Raylib.CheckCollisionRecs(_Position, other);
    }

    public bool IsCollidingWith(Vector2 otherCenter, float otherRadius)
    {
        return Raylib.CheckCollisionCircleRec(otherCenter, otherRadius, _Position);
    }
    

    public XmlElement ToXML(XmlDocument document)
    {
        XmlElement node = document.CreateElement("alma");
        node.SetAttribute("x", _Position.X.ToString());
        node.SetAttribute("y", _Position.Y.ToString());

        return node;
    }

    public bool IsCollidingWith(Vector2 lineStart, Vector2 lineEnd, float lineThickness)
    {
        return Raylib.CheckCollisionPointLine(_Position.Position, lineStart, lineEnd, 10);
    }
}