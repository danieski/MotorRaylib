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
        private Vector2 Posicion {  get => posicion; set => posicion=value; }
        private int radio;
        public int Radio {  get => radio; set => radio=value; }
        
        public GameObject ( Vector2 posicion, int radio)
        {

            this.posicion = posicion;
            this.radio = radio;
           
        }

        public abstract Vector2 update(float deltaTime);
        public abstract void render();
       

    }
}
