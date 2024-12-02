using motoret;
using Raylib_cs;

namespace exempleClasses;
public class ExempleClasses
{
    public static Texture2D TexturaAlma
    {
        get; private set;
    }

    public static void Run(bool loadFromFile = false)
    {
        Motoret motor = Motoret.Instance;

        //inicialitzar el motor (finestra, valors, etc)
        motor.Init();

        //carreguem la textura que serà comuna per a totes les Alma.
        //això seria millor tenir un gestor de recursos/assets al propi motor
        //per tal que ell mateix en gestioni la càrrega/descàrrega i accés
        TexturaAlma = Raylib.LoadTexture("assets/alma.png");

        if(!loadFromFile)
        {
            //creació dels GameObjects que ens interessen
            //Aquest d'aquí equival a l'antic player
            IGameObject player = new Mousus();
            
            motor.AddGameObject(player);

            //aquest s'encarregarà de la gestió del botó de sortida del joc
            motor.AddGameObject(new ExitButton());

            int width  = Raylib.GetScreenWidth();
            int height = Raylib.GetScreenHeight();

            for(int i = 0; i < 100; ++i)
                motor.AddGameObject(new Alma(TexturaAlma, width, height));
        }else
        {
            motor.AddGameObject(XMLExporter.LoadFile("savegame.xml"));
        }

        //iniciem el joc
        motor.Run();

        //hauriem de canviar això a part del motor per tal que sigui capaç de gestionar ell els recursos comuns
        Raylib.UnloadTexture(TexturaAlma);

        //quan el joc acabi, alliberem recursos.
        motor.Close();
    }
}