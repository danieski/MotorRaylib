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


        /* SINGLETON ENGINE */

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

        public void run()
        {
            while (!Raylib.WindowShouldClose())
            {

                update();
                


                /*RENDER*/
                fisicas();
                //Raylib.BeginMode2D(camera);
                render();
                //Raylib.EndMode2D();
                //gestionar nuevos objetos u objetos a borrar
                //Add objeto
                //delete

            }
        }

        /* UPDATE */
        public void update()
        {
            float deltaTime = Raylib.GetFrameTime();
            foreach (GameObject objLista in listGameObjets)
            {
                objLista.update();
            }
            
        }


        /* RENDER */
        public void render()
        {
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 650, 50, 20, Color.Pink);

            foreach (GameObject objLista in listGameObjets)
            {

                
                objLista.render();


            }

            Raylib.EndDrawing();
        }

        public void addGameObject(GameObject gameObject)
        {
            listGameObjets.Add(gameObject);
        }
        public void removeGameObject(GameObject gameObject)
        {
            listGameObjets.Remove(gameObject);
        }
        /* FISICAS */
        public void fisicas()
        {

            GameObject personaje = listGameObjets[0];
            GameObject enemigo = listGameObjets[1];


                if (Raylib.CheckCollisionCircles(personaje.posicion, personaje.radio, enemigo.posicion, enemigo.radio))
                {
                    Raylib.DrawText("Collision Detected", 400, 220, 20, Color.Red);
                }

        }


    }
}
