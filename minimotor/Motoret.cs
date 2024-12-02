using System.Reflection;
using Raylib_cs;
using System.Diagnostics;
using System.Numerics;
using System.Collections.ObjectModel;

namespace motoret;


public class Motoret
{

    private static Motoret _Instance;
    public static Motoret Instance
    {
        get
        {
            if(_Instance == null)
                new Motoret();
            
            return _Instance;
        }
    }

    private List<IGameObject> _GameObjects;
    private List<IGameObject> _GameObjectsToAdd;
    private List<IGameObject> _GameObjectsToRemove;
    private List<IPhysicGameObject> _PhysicGameObjects;

    //Per a simplificar fem una càmera per a tota l'escena
    private Camera2D _Camera;
    private bool _Stop;

    private Motoret()
    {
        _Instance = this;
        _GameObjects = new();
        _GameObjectsToRemove = new();
        _GameObjectsToAdd = new();
        _PhysicGameObjects = new();
    }

    public void Stop()
    {
        _Stop = true;
    }

    public void CameraPosition(Vector2 position)
    {
        _Camera.Target = position;
    }

    public void Init(int width = 800, int height = 480, int fps = 60, string name = "Motoret 0.1")
    {
        Raylib.InitWindow(width, height, name);
        Raylib.SetTargetFPS(fps);
        _Camera = new Camera2D
        {
            Target = Vector2.Zero,
            Offset = new Vector2 { X= Raylib.GetScreenWidth()/2.0f, Y= Raylib.GetScreenHeight()/2.0f },
            Rotation = 0.0f,
            Zoom = 1.0f
        };
    }

    public void AddGameObject(IGameObject gameObject)
    {
        Debug.Assert(gameObject is not null);

        _GameObjectsToAdd.Add(gameObject);
    }

    public void AddGameObject(List<IGameObject> gameObjects)
    {
        foreach(IGameObject gameObject in gameObjects)
            AddGameObject(gameObject);
    }

    private void AddGameObjects()
    {
        foreach (IGameObject gameObject in _GameObjectsToAdd)
        {
            _GameObjects.Add(gameObject);
            if(gameObject is IPhysicGameObject)
                _PhysicGameObjects.Add(gameObject as IPhysicGameObject);

            gameObject.Start();
        }
        _GameObjectsToAdd.Clear();
    }

    public ReadOnlyCollection<IGameObject> GetGameObjects()
    {
        return _GameObjects.AsReadOnly();
    }

    public void RemoveGameObject(IGameObject gameObject)
    {
        _GameObjectsToRemove.Add(gameObject);
    }

    private void RemoveGameObjects()
    {
        foreach (IGameObject gameObject in _GameObjectsToRemove)
        {
            _GameObjects.Remove(gameObject);
            if(gameObject is IPhysicGameObject)
                _PhysicGameObjects.Remove(gameObject as IPhysicGameObject);
            gameObject.Dispose();    
        }
        _GameObjectsToRemove.Clear();
    }

    //Aquest motoret no funciona si afegiu coses en calent.
    public void Run()
    {
        while (!Raylib.WindowShouldClose() && !_Stop)
        {
            //Esborrem tots els gameObjects a esborrar
            RemoveGameObjects();
            //Afegim tots els gameObjects creats
            AddGameObjects();
            
            //Update
            foreach(IGameObject gameObject in _GameObjects)
                gameObject.Update();

            //Game State
 
            //Físiques
            for(int i = 0; i < _PhysicGameObjects.Count; i++)
            {
                for(int j=i+1; j < _PhysicGameObjects.Count; j++)
                {
                    if(_PhysicGameObjects[i].IsCollidingWith(_PhysicGameObjects[j]))
                    {
                        _PhysicGameObjects[i].HasCollidedWith(_PhysicGameObjects[j]);
                        _PhysicGameObjects[j].HasCollidedWith(_PhysicGameObjects[i]);
                    }
                }
            }

            //Render sota càmera
            Raylib.BeginMode2D(_Camera);
                Raylib.ClearBackground(Color.Black);

                foreach(IGameObject gameObject in _GameObjects)
                    gameObject.Render();

            Raylib.EndMode2D();

            //Render sota GUI
            Raylib.BeginDrawing();

                foreach(IGameObject gameObject in _GameObjects)
                    gameObject.RenderGUI();

            Raylib.EndDrawing();
        }
    }

    public void Close()
    {
        while(_GameObjects.Count > 0)
        {
            IGameObject gameObject = _GameObjects[0];
            gameObject.Dispose();
            _GameObjects.RemoveAt(0);
            if(gameObject is IPhysicGameObject)
                _PhysicGameObjects.Remove(gameObject as IPhysicGameObject);
        }
        Raylib.CloseWindow();
    }
}