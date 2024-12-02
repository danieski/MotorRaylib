using exempleDirecte;
using exempleClasses;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace motoret;
class Program
{
    public static void Main()
    {
        //exemple as-is tot directe al codi principal
        //ExempleDirecte.Run();

        //exemple muntant una petita estructura de motor
        ExempleClasses.Run();

        //aquest carrega la partida de fitxer
        //si no heu executat i desat a l'exemple anterior, no podreu carregar res
        //ExempleClasses.Run(true);
    }
}

