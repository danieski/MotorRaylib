using System.Numerics;
using System.Xml;
using motoret;
using Raylib_cs;

namespace exempleClasses;

public class Bala : IGameObject, IPhysicGameObject
{
    private Vector2 _Position;
    private Vector2 _Speed;
    private Color _Color = Color.Green;
    private float TTL=3000f;
    private bool _balaIsActive = true;
    
    public Bala(Vector2 position, Vector2 speed)
    {   
        _Position = position;
        _Speed = speed;
        Console.WriteLine("Bala created");
    }

    public void Start()
    {
    }

    public void Update()
    {
        //Console.WriteLine("La bala esta activdad: " +  _balaIsActive);
        _Position += _Speed * Raylib.GetFrameTime();
        TTL -= 0.1f * Raylib.GetFrameTime();
        if (TTL < 0)
            Motoret.Instance.RemoveGameObject(this);
       // Console.WriteLine("Posicion de la bala: " +  _Position);
        
    }

    public void Render()
    {
        if (_balaIsActive)
            Raylib.DrawRectangle((int)_Position.X, (int)_Position.Y,10,10,_Color);   
    }

    public void RenderGUI()
    {
    }

    public void Dispose()
    {
    }

    public XmlElement ToXML(XmlDocument document)
    {
        
        throw new NotImplementedException();
        XmlElement node = document.CreateElement("alma");

        return node;
    }

    public bool IsCollidingWith(IPhysicGameObject other)
    {
        return false;
    }

    public bool IsCollidingWith(Rectangle other)
    {
        //_Color = Color.Pink;
        
        return Raylib.CheckCollisionRecs(new Rectangle(_Position.X,_Position.Y,10,10), other);
    }

    public bool IsCollidingWith(Vector2 otherCenter, float otherRadius)
    {
        return false;
    }

    public void HasCollidedWith(IPhysicGameObject other)
    {
        _Color = Color.Red;
        Motoret.Instance.RemoveGameObject(this);
    }

    public bool IsCollidingWith(Vector2 lineStart, Vector2 lineEnd, float lineThickness)
    {
        return false;
    }

    public void activateBala(Vector2 PosicionA, Vector2 Direccion)
    {
        _balaIsActive = !_balaIsActive;
        _Position = PosicionA;
        _Speed = Direccion;
    }
}