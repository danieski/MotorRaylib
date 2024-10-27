using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace motor
{
    public class Enemigo : GameObject
    {
        
        public Enemigo(Vector2 posicion, int radio) : base(posicion, radio)
        {
            
            
        }

        public override void update()
        {
            /*MOVIMIENTO ENEMIC*/

            //Que se le aleje del personaje

            this.posicion.X += MovimientoGOX(30);
            this.posicion.Y += MovimientoGOY(30);
            //this.posicion.X += personaje.movimientoPersonajeX(Raylib.GetFrameTime());
            //Raylib.coll
        }
        
        public override void render()
        {
            Raylib.DrawCircleV(this.posicion, this.radio, Color.Brown);
        }
        /*
        public override float movimientoPersonajeX(float deltaTime)
        {
            float direccionX = 0;

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                direccionX = 50 * deltaTime;
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                direccionX = -50 * deltaTime;
            }
            return direccionX;
        }*/
    }
}
    
