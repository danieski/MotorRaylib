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
        _Position.X = (width/2 - Random.Shared.Next(width)) * 3;
        _Position.Y = (height/2 - Random.Shared.Next(height)) * 3;

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
        _Position.Height = 100;
        _Position.Width = 100;

        _Texture = texture;

        _ColorHit   = Color.Red;
        _ColorNoHit = Color.White;
        _ColorCurrent = _ColorNoHit;

        //info de dibuix
        _DrawRectangleInfo = new Rectangle {X = 0, Y = 0, Width = _Texture.Width, Height = _Texture.Height};
    }

    public void Start() { }

    public void Update()
    { 
        //_ColorCurrent = _ColorNoHit;
    }

    //Al ser textura compartida, no l'alliberem
    public void Dispose() { }

    public void Render()
    {
        Raylib.DrawTexturePro(_Texture, _DrawRectangleInfo, _Position, Vector2.Zero, 0, _ColorCurrent);
        switch (_currentLifeBar)
        {
            case LifeBar.zero:
                _ColorCurrent = Color.Pink;
                break;
            case LifeBar.one:
                _ColorCurrent = Color.Purple;
                break;
            case LifeBar.two:
                _ColorCurrent = Color.Violet;
                break;
            case LifeBar.three:
                _ColorCurrent = Color.DarkPurple;
                break;
            case LifeBar.four:
                motoret.Motoret.Instance.RemoveGameObject(this);
                break;
        }
        Raylib.DrawRectangleLinesEx(_Position, 5, _ColorCurrent);
    }

    public void RenderGUI() { }

    //PhysicGameObject
    public void HasCollidedWith(IPhysicGameObject other)
    {
 
        if (other is Bala)
        {
            _currentLifeBar += 1;
        }
        
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
}