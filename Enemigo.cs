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
        
        public override bool comprobarColisionConOtro(GameObject obj)
        {
            //comprobar las colisiones con el otro es
            //llamar a la funcion de colision con rectangulo
            //porque yo soy rectangulo
            return obj.comprobarColisionConRectangulo(rectangulo); 
            
            
            /*
             
            if(obj is Enemigo)
                return Raylib.CheckCollisionRecs(this.rectangulo, obj.rectangulo);
            if(obj is Personaje)
                return Raylib.CheckCollisionCircleRec(obj.centro, obj.radio, rectangulo);
        */
        }
        //Esta funcion por ejemplo sera llamada por el circulo que colisione con nosotros
        //introducira su informacion Vector y radio 
        //Como ya sabemos que somos un rectangulo entonces el circulo que chcoque con nosotros
        //recibira para que ejeecute una funcion que compruebe colision rectangulo con circulo
        public override bool comprobarColisionConCirculo(Vector2 centro, float radio)
        {
            return Raylib.CheckCollisionCircleRec(centro, radio, rectangulo);
        }
        
        public override bool comprobarColisionConRectangulo(Rectangle rectangulo)
        {
            return Raylib.CheckCollisionRecs(this.rectangulo, rectangulo);
        }
    }
}
    
