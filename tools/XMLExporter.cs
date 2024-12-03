using System.Collections.ObjectModel;
using System.Xml;
using motoret;

namespace exempleClasses;

public static class XMLExporter
{
    public static void SaveFile(string filePath, ReadOnlyCollection<IGameObject> gameObjects)
    {
        try
        {
            XmlDocument document = new XmlDocument();
            XmlElement level = document.CreateElement("level");

            document.AppendChild(level);

            foreach(IGameObject gameObject in gameObjects)
                level.AppendChild(gameObject.ToXML(document));

            document.Save(filePath);
        }catch (Exception e)
        {
            Console.Write($"Error d'exportació del fitxer {filePath}.");
            Console.Write(e);
        }
    }

    public static List<IGameObject> LoadFile(string filePath)
    {
        List<IGameObject> gameObjects = new();

        try
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            XmlNode levelNode = document["level"];
            
            foreach (XmlNode gameObjectNode in levelNode)
            {
                switch(gameObjectNode.Name)
                {
                    case "mousus":
                        gameObjects.Add(new Personaje(gameObjectNode));
                        break;
                    case "alma":
                        gameObjects.Add(new Alma(gameObjectNode));
                        break;
                    case "exitbutton":
                        gameObjects.Add(new ExitButton());
                        break;
                }
            }
            
        } catch (Exception e)
        {
            Console.Write($"Error d'importació del fitxer {filePath}.");
            Console.Write(e);
        }

        return gameObjects;
    }
}