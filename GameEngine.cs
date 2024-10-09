using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace motor
{
    public class GameEngine
    {
        List<GameObject> listGameObjets = new List<GameObject>();

        public GameObject personaje = new Personaje(new Vector2(50, 50), 25);
        
        const int CIRCLE_SPEED = 50;
        float deltaTime = Raylib.GetFrameTime();

        /* SINGLETONE ENGINE */

        private static GameEngine instance;
        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEngine();

                }
                return Instance;
            }
        }

        /* INIT */

        public void start()
        {

            Raylib.SetTargetFPS(60);
            Raylib.InitWindow(800, 480, "Cops and Thifs");
            

        }
        
        /* UPDATE */
        public void update(float deltaTime)
        {
            /*
            foreach (GameObject objLista in listGameObjets)
            {
                objLista.posicion = personaje.update(deltaTime);
            }
            */
           personaje.posicion = personaje.update(deltaTime);
        }

        
        public void addGameObject()
        {
            listGameObjets.Add(personaje);
        }
        public void removeGameObject()
        {

        }

        /* RENDER */
        public void render()
        {
            /*
            foreach (Personaje objLista in gameObjects)
            {
                objLista.render(objLista.posicionpersonaje);
            }
            */
            personaje.render();
        }
        /* SHUTDOWN */
    }
}
