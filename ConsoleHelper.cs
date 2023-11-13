using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet
{
    public class ConsoleHelper
    {
        public static void WriteAt(string s, int x, int y, int origCol=0, int origRow=0)
        {

            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public static ConsoleColor GetConsoleColor(string zona)
        {
            List<ConsoleColor> colors = new List<ConsoleColor>();
            List<string> nombres = new List<string>();
            Array arr = Enum.GetValues(typeof(EnumColoresZona));
            Array arrNombre = Enum.GetNames(typeof(EnumColoresZona));
            foreach (ConsoleColor cc in arr)
            {
                colors.Add(cc);
            }
            foreach (string nombre in arrNombre)
            {
                nombres.Add(nombre);
            }
            return colors[nombres.IndexOf(zona)];
        }
    }
}
