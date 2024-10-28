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
        
        public override bool comprobarColisionConOtro(GameObject obj)
        {
            //poruque? porque comprueba col con circulo
            //AH! porque el otro colisionara con circulo
            
            return obj.comprobarColisionConCirculo(posicion, radio);
        }
        //que de hecho es este 
        //si estoy colisionando con otro
        //de este otro (obj) voy a ejecutar su funcion implementada comprobarColisionConCirculo
        public override bool comprobarColisionConCirculo(Vector2 centro, float radio)
        {
            
            return Raylib.CheckCollisionCircles(this.posicion, this.radio, centro, radio);
        }
        //si el otro es un rectangulo el llamara a la funcion comprobarColisionConRectangulo
        //Poruqe? porque el mismo me va a decir que lo compruebe, el sabe que con lo que estas colisionando es un rectangulo
        //porque el es un rectangulo
        public override bool comprobarColisionConRectangulo(Rectangle rectangulo)
        {
            return Raylib.CheckCollisionCircleRec(this.posicion, this.radio, rectangulo);
        }
    }
}
