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
    
    public Bala(Vector2 position, Vector2 speed)
    {   
        _Position = position;
        _Speed = speed;
    }

    public void Start()
    {
    }

    public void Update()
    {
        _Position += _Speed * Raylib.GetFrameTime();
        
    }

    public void Render()
    {
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
        motoret.Motoret.Instance.RemoveGameObject(this);
    }
}