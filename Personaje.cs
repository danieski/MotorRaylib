using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace motor
{
    public class Personaje : GameObject
    {
        public float tiempoAcumulado = 0f;
        private float intervaloCambioColor = 1f; // Cambiará de color cada segundo
        public Color colorActual = Color.Blue;  // Color inicial

        public Vector2 posicion;
        //private Vector2 Posicion { get => posicion; set => posicion = value; }
        public Personaje(Vector2 posicion, int radio) : base(posicion, radio)
        {
        }
        /* UPDATE */
        public override Vector2 update(float deltaTime)

        {

            /* MOVIMIENTO */

            float cambioPosicionY = movimientoPersonajeY(deltaTime);
            float cambioPosicionX = movimientoPersonajeX(deltaTime);
            this.posicion.Y += cambioPosicionY;
            this.posicion.X += cambioPosicionX;
            

            /*SIRENAS DE POLI*/

            tiempoAcumulado += deltaTime;
            if (tiempoAcumulado >= intervaloCambioColor)
            {
                if (colorActual.Equals(Color.Blue)) 
                    {
                        colorActual = Color.Red;
                    }
                 else
                    {
                        colorActual = Color.Blue;
                    }
                tiempoAcumulado = 0f;
            }
            
            return new Vector2(this.posicion.X, this.posicion.Y);
        }
        /* RENDER */
        public override void render()
        {

            
            //contadorCaminar++;
            Raylib.DrawCircleV(this.posicion, 50, colorActual);
            
        }
        /* INPUTS */

        /*INPUTS EJE Y*/
        public float movimientoPersonajeY(float deltaTime)
        {
           
            float direccionY = 0;

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                direccionY = 50 * deltaTime;
            }
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                direccionY = -50 * deltaTime;
            }
            return direccionY;

        }

        /*INPUTS EJE X*/
        public float movimientoPersonajeX(float deltaTime)
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


        }
    }
}
