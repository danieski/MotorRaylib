using System.Numerics;
using System.Xml;
using Raylib_cs;

namespace motoret;

public interface IGameObject
{
    public void Start();
    public void Update();
    public void Render();
    public void RenderGUI();
    public void Dispose();
    public XmlElement ToXML(XmlDocument parent);
}

public interface IPhysicGameObject
{
    public bool IsCollidingWith(IPhysicGameObject other);
    public bool IsCollidingWith(Rectangle other);
    public bool IsCollidingWith(Vector2 otherCenter, float otherRadius);
 
    public void HasCollidedWith(IPhysicGameObject other);
    public bool IsCollidingWith(Vector2 lineStart, Vector2 lineEnd, float lineThickness);
}

