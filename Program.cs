using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using motor;
using Raylib_cs;

namespace motoret;
class Program
{
    const int CIRCLE_SPEED = 50;

    /* PROGRAMA PRINCIPAL */
    public static void Main()

    {

        /* INICIAMOS MOTOR*/
        /* CONFIG */
        GameEngine engine = new GameEngine();

        engine.start();
             

        int contadorCaminar = 0;

        /* GAME LOOP */

        while (!Raylib.WindowShouldClose())

        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            float deltaTime = Raylib.GetFrameTime();
            Raylib.DrawText($"Deltatime: {deltaTime}", 100, 300, 20, Color.Pink);





            /*UPDATE*/
            engine.addGameObject();
            engine.update(deltaTime);

            //engine.movimientoPersonaje(deltaTime, direccionX, direccionY);
            //personaje.Posicion = personaje.update(deltaTime, direccionX, direccionY);

            /*RENDER*/
            
            Raylib.DrawText($"Delta time: {deltaTime}", 550, 20, 20, Color.Red);

            engine.render();
            contadorCaminar++;
            if (contadorCaminar > 20)
                contadorCaminar = 0;




            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
       
        //engine.run();  

        

        /*
        Raylib.InitWindow(800, 480, "El primer motoret");

        

        //Player
        Rectangle playerPosition= new Rectangle {X = Raylib.GetScreenWidth()/2, Y = Raylib.GetScreenHeight()/2, Height = 100, Width = 100};
        Texture2D textureNoHit  = Raylib.LoadTexture("assets/chonkus.png");
        Texture2D textureHit    = Raylib.LoadTexture("assets/mousus.png");
        Texture2D texture       = textureNoHit;
        Rectangle playerTexRec  = new Rectangle {X = 0, Y = 0, Width = textureNoHit.Width, Height = textureNoHit.Height};

        //Obstacles
        Rectangle[] collisionAreas = new Rectangle[100];
        {
            int width  = Raylib.GetScreenWidth();
            int height = Raylib.GetScreenHeight();

            for(int i = 0; i < collisionAreas.Length; i++)
            {
                collisionAreas[i].Height = 100;
                collisionAreas[i].Width = 100;
                collisionAreas[i].X = (width/2 - Random.Shared.Next(width)) * 3;
                collisionAreas[i].Y = (height/2 - Random.Shared.Next(height)) * 3;
            }
        }
            
        Texture2D textureCol    = Raylib.LoadTexture("assets/alma.png");
        Rectangle collisionRec  = new Rectangle {X = 0, Y = 0, Width = textureCol.Width, Height = textureCol.Height};

        //camera2d
        Camera2D camera = new();

        camera.Target = playerPosition.Position;
        camera.Offset = new Vector2 { X= Raylib.GetScreenWidth()/2.0f, Y= Raylib.GetScreenHeight()/2.0f };
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        //GUI
        Rectangle exitButton = new Rectangle { X = Raylib.GetScreenWidth() - 60, Y = 40, Width = 20, Height = 20 };
        Color exitButtonColor = Color.Blue;

        //config
        float deltaTime = 0;
        Raylib.SetTargetFPS(60);

        bool exit = false;
        while (!Raylib.WindowShouldClose() && !exit)
        {
            deltaTime = Raylib.GetFrameTime();

            //Movement
            if(Raylib.IsKeyDown(KeyboardKey.S))
                playerPosition.Y += CIRCLE_SPEED*deltaTime;
            if(Raylib.IsKeyDown(KeyboardKey.W))
                playerPosition.Y -= CIRCLE_SPEED*deltaTime;
            if(Raylib.IsKeyDown(KeyboardKey.D))
                playerPosition.X += CIRCLE_SPEED*deltaTime;
            if(Raylib.IsKeyDown(KeyboardKey.A))
                playerPosition.X -= CIRCLE_SPEED*deltaTime;

            camera.Target = playerPosition.Position;

            //Physics
            texture = textureNoHit;
            foreach(Rectangle collisionArea in collisionAreas)
            {
                if(Raylib.CheckCollisionCircleRec(playerPosition.Position, 50, collisionArea))
                {
                    texture = textureHit;
                    break;
                }
            }

            //GUI
            if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), exitButton))
            {
                if(Raylib.IsMouseButtonReleased(MouseButton.Left))
                    exit = true;
                
                exitButtonColor = Color.Red;
            }else{
                exitButtonColor = Color.Blue;
            }

            //Render de càmera
            Raylib.BeginMode2D(camera);
                Raylib.ClearBackground(Color.White);

                foreach(Rectangle collisionArea in collisionAreas)
                {
                    Raylib.DrawTexturePro(textureCol, collisionRec, collisionArea, Vector2.Zero, 0, Color.White);
                    Raylib.DrawRectangleLinesEx(collisionArea, 5, Color.Pink);
                }

                //1: textura a utilitzar
                //2: zona de la textura a utilitzar (per si hi ha sprites i fragmentem)
                //3: partint del tamany determinat per 2, on situem l'origen/offset
                //4: rotació
                //5: tintat
                Raylib.DrawTexturePro(texture, playerTexRec, playerPosition, new Vector2 {X = 50, Y = 50}, 0, Color.White);
                Raylib.DrawCircleLinesV(playerPosition.Position, 50, Color.Lime);
                //Raylib.DrawCircleV(position, 50, color);
            Raylib.EndMode2D();

            //Render de pantalla, GUI
            Raylib.BeginDrawing();

                Raylib.DrawText($"Position: ({playerPosition.X},{playerPosition.Y})", 12, 12, 20, Color.Black);

                Raylib.DrawRectangleRec(exitButton, exitButtonColor);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(textureHit);
        Raylib.UnloadTexture(textureNoHit);
        Raylib.UnloadTexture(textureCol);
        
        Raylib.CloseWindow();
        */

