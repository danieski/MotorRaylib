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
        /* COONFIG */
        public float tiempoAcumulado = 0f;
        private float intervaloCambioColor = 1f; // Cambiará de color cada segundo
        public Color colorActual = Color.Blue;  // Color inicial
                                                //camera2d
        

        public Personaje(Vector2 posicion, int radio) : base(posicion, radio)
        {
        }
        /* UPDATE */
        public override void update()
        {

            /* MOVIMIENTO */

            //GameEngine.Instance.CamaraPosition(position.X, position.Y)
            this.posicion.Y += MovimientoGOY(50);
            this.posicion.X += MovimientoGOX(50);

            /* MOVIMIENTO CAMARA */

            //camera.Target = this.posicion;

            /*SIRENAS DE POLI*/

            tiempoAcumulado += Raylib.GetFrameTime();
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

        }

        /* RENDER */
        public override void render()
        {
            Raylib.DrawCircleV(this.posicion, this.radio, colorActual);
        }


        
    }
}
