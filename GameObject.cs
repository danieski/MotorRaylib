using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace motor
{
    public abstract class GameObject
    {
        public Vector2 posicion;
        public int radio;
      

        
        public GameObject ( Vector2 posicion, int radio)
        {

            this.posicion = posicion;
            this.radio = radio;
           
        }

        public abstract void update();
        public abstract void render();
        public float MovimientoGOX(float velocidad)
        {
            float direccionX = 0;

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                direccionX = velocidad * Raylib.GetFrameTime();
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                direccionX = -velocidad * Raylib.GetFrameTime();
            }
            return direccionX;
        }
        public float MovimientoGOY(float velocidad)
        {
            float direccionY = 0;

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                direccionY = velocidad * Raylib.GetFrameTime();
            }
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                direccionY = -velocidad * Raylib.GetFrameTime();
            }
            return direccionY;
        }

        



    }
}
