**Submarine Radar - Raylib**
**Descripción**

El objetivo principal de este proyecto es entender los componentes fundamentales de los motores de videojuegos, incluyendo elementos que participan en su ejecución, como el bucle principal, el sistema de renderizado y el pooling de objetos. Raylib ayuda a gestionar algunos de los aspectos más desafiantes del desarrollo de videojuegos desde cero, tales como la física y la gestión de recursos. Además del pooling y otros aspectos, estamos utilizando una forma sencilla de guardar texturas en formato XML, lo cual permite cargar y descargar texturas en Raylib de manera eficiente.

Con este código, logramos agregar nuevos objetos al bucle principal, definir interfaces que establecen las propiedades de los objetos, como IGameObject e IPhysicGameObject, y gestionar las colisiones. Estas colisiones se verifican mediante un bucle que recorre todos los objetos del juego y, utilizando el patrón Visitor, se comprueba si algún objeto está colisionando con otro en la lista.

Usando las funciones Math().cos() y Math.sen() se realiza la logica de impacto del radar con lo que calculara en que momento impacta con un objetivo para este ser renderizado por el programa, use esta misma logica para 
dirigir los disparos de mi submarino.


**Patrones de Programación Utilizados:**

- [X] Object Pool
- [X] Singleton
- [X] State Machine
- [X] Visitor
- [ ] Factory
- [ ] Observer
- [ ] Object Component
  
**Instrucciones:**

La barra de radar aparecerá y revelará los obstáculos en tu camino. Selecciona la mejor ruta para evitarlos y destrúyelos para ganar puntos.

**Controles:**

A, D → Rotar

W → Avanzar

P → Pausar

Flecha Arriba → Disparar

T → Turbo (Aumentar velocidad, no permite disparar)

**Cambios Fututos:**

Para entregar el codigo antes de la deadline algunas de las ultimas features se presentan fuera de la estructura principal del programa. Se deberan introducir en la estructura principal. Por ejemplo la parte del GUI recien agregada deberia estar en una clase separada en la que esta actualmente es motot.cs

Añadir una tabla de puntuaciones altas.
Mejorar los sprites para el jugador y los obstáculos.
Añadir más pools de objetos para una mejor gestión de recursos.

----------------------------------------------------------------------------

**English**

**Submarine Radar - Raylib**

**Description**

The main objective of this project is to understand the core components of game engines, including elements involved in their execution, such as the main loop, the rendering system, and object pooling. Raylib helps manage some of the more challenging aspects of game development from scratch, such as physics and asset management. In addition to pooling and other features, we are using a simple method to save textures in XML format, allowing for efficient texture loading and unloading in Raylib.

With this code, we manage to add new objects to the main loop, define interfaces that set the properties of objects, such as IGameObject and IPhysicGameObject, and handle collisions. These collisions are verified through a loop that iterates over all game objects and, using the Visitor pattern, checks if any object is colliding with another in the object list.

**Trigonometry**

We have implemented a radar line to detect obstacles, represented as a single line. The starting point of this line is the main character, and the endpoint is set to be 100 pixels away from him. The line rotates based on a given angle.

To compute the position of the endpoint, we use trigonometric functions: cosine and sine. These functions represent the x and y components of the line's direction. Specifically, we calculate the x and y coordinates by multiplying the cosine and sine of the radar angle (_radarAngle) by the distance (100 pixels). The resulting x and y values give us the final position of the radar line's endpoint.

Similarly, we apply the same principle to determine the submarine's shooting direction. As the user inputs control commands, the submarine's shooting direction rotates accordingly, and we adjust the shooting angle using the cosine and sine functions in the same way.

**Programming Patterns Used:**

- [X] Object Pool
- [X] Singleton
- [X] State Machine
- [X] Visitor
- [ ] Factory
- [ ] Observer
- [ ] Object Component

**Instructions:**

The radar bar will appear and reveal obstacles in your path. Select the best route to avoid them and destroy them to gain points.

**Controls:**

A, D → Rotate

W → Move Forward

P → Pause

Up Arrow → Shoot

T → Turbo (Increase speed, disallows shooting)

**Future Changes:**

Add a high score board.
Improve sprites for the player and obstacles.
Add more object pools for better resource management.
