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
        public static Dictionary<char, string[]> Letras {  get; set; } = new Dictionary<char, string[]>
        {
            { 'A', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @  @",
                    "@     @",
                    "@  @  @",
                    "@  @  @",
                    "@@@@@@@",
                    "@@@ @@@"
                }
            },
            { 'B', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @  @",
                    "@    @@",
                    "@  @  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'C', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @@@@",
                    "@  @@@@",
                    "@  @@@@",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'D', new string[]
                {
                    "@@@@@@ ",
                    "@    @@",
                    "@  @  @",
                    "@  @  @",
                    "@  @  @",
                    "@    @@",
                    "@@@@@@@",
                    "@@@@@@ "
                }
            },
            { 'E', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @@@@",
                    "@    @ ",
                    "@  @@@@",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'F', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @@@@",
                    "@    @@",
                    "@  @@@ ",
                    "@  @@@ ",
                    "@@@@@@  ",
                    "@@@@@@  "
                }
            },
            { 'G', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @@@@",
                    "@  @@@@",
                    "@  @  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'H', new string[]
                {
                    "@@@@@@@",
                    "@  @  @",
                    "@  @  @",
                    "@     @",
                    "@  @  @",
                    "@  @  @",
                    "@@@@@@@",
                    "@@@ @@@"
                }
            },
            { 'I', new string[]
                {
                    "@@@@",
                    "@  @",
                    "@  @",
                    "@  @",
                    "@  @",
                    "@  @",
                    "@@@@",
                    "@@@@"
                }
            },
            { 'J', new string[]
                {
                    "   @@@@",
                    "   @  @",
                    "   @  @",
                    "@@@@  @",
                    "@  @  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'K', new string[]
                {
                    "@@@@@@@",
                    "@  @  @",
                    "@  @  @",
                    "@    @@",
                    "@  @  @",
                    "@  @  @",
                    "@@@@@@@",
                    "@@@ @@@"
                }
            },
            { 'L', new string[]
                {
                    "@@@@   ",
                    "@  @   ",
                    "@  @   ",
                    "@  @   ",
                    "@  @@@@",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'M', new string[]
                {
                    "@@@@ @@@@",
                    "@  @@@  @",
                    "@   @   @",
                    "@       @",
                    "@  @ @  @",
                    "@  @@@  @",
                    "@@@@@@@@@",
                    "@@@@ @@@@"
                }
            },
            { 'N', new string[]
                {
                    "@@@@@@@@",
                    "@  @@  @",
                    "@   @  @",
                    "@      @",
                    "@  @   @",
                    "@  @@  @",
                    "@@@@@@@@",
                    "@@@@@@@@"
                }
            },
            { 'O', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @  @",
                    "@  @  @",
                    "@  @  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'P', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @  @",
                    "@     @",
                    "@  @@@@",
                    "@  @@@@",
                    "@@@@@   ",
                    "@@@@@   "
                }
            },
            { 'Q', new string[]
                {
                    "@@@@@@@ ",
                    "@     @ ",
                    "@  @  @ ",
                    "@  @  @ ",
                    "@  @  @@",
                    "@      @",
                    "@@@@@@@@",
                    "@@@@@@@@"
                }
            },
            { 'R', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @  @",
                    "@    @@",
                    "@  @  @",
                    "@  @  @",
                    "@@@@@@@",
                    "@@@ @@@"
                }
            },
            { 'S', new string[]
                {
                    "@@@@@@@",
                    "@     @",
                    "@  @@@@",
                    "@     @",
                    "@@@@  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'T', new string[]
                {
                    "@@@@@@@@",
                    "@      @",
                    "@@@  @@@",
                    "@@@  @@@",
                    "  @  @  ",
                    "  @  @  ",
                    "  @@@@  ",
                    "  @@@@  "
                }
            },
            { 'U', new string[]
                {
                    "@@@@@@@",
                    "@  @  @",
                    "@  @  @",
                    "@  @  @",
                    "@  @  @",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }
            },
            { 'V', new string[]
                {
                    "@@@@@@@",
                    "@  @  @",
                    "@  @  @",
                    "@  @  @",
                    "@     @",
                    "@@@ @@@",
                    " @@@@@ ",
                    "  @@@  "
                }
            },
                { 'W', new string[] {
                    "@@@@ @@@@",
                    "@  @@@  @",
                    "@  @ @  @",
                    "@       @",
                    "@   @   @",
                    "@  @@@  @",
                    "@@@@@@@@@",
                    "@@@@ @@@@"
                }},

                { 'X', new string[] {
                    "@@@@@@@",
                    "@  @  @",
                    "@  @  @",
                    "@@   @@",
                    "@  @  @",
                    "@  @  @",
                    "@@@@@@@",
                    "@@@ @@@"
                }},

                { 'Y', new string[] {
                    "@@@@@@@@",
                    "@  @@  @",
                    "@  @@  @",
                    "@@    @@",
                    "@@@  @@@",
                    " @@  @@ ",
                    "  @  @  ",
                    "  @@@@  "
                }},

                { 'Z', new string[] {
                    "@@@@@@@",
                    "@     @",
                    "@@@@  @",
                    "@     @",
                    "@  @@@@",
                    "@     @",
                    "@@@@@@@",
                    "@@@@@@@"
                }},

                { '!', new string[] {
                    "@@@@",
                    "@  @",
                    "@  @",
                    "@  @",
                    "@@@@",
                    "@  @",
                    "@@@@",
                    "@@@@"
                }},

                { ' ', new string[] {
                    "    ",
                    "    ",
                    "    ",
                    "    ",
                    "    ",
                    "    ",
                    "    ",
                    "    "
                }},
                { '.', new string[] {
                        "     ",
                        "     ",
                        "     ",
                        "     ",
                        "     ",
                        " @@@ ",
                        " @ @ ",
                        " @@@ "
                    }}
            };
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
        private static int GetEspaciosBlancosParaCentrarTexto(int caracteres)
        {
            int caracteresGrandes = Console.WindowWidth / 7;
            int espaciosBlancos = (caracteresGrandes - caracteres) / 2;
            return espaciosBlancos*2;
        }
        public static void WriteTitulo(string titulo, ConsoleColor color)
        {
            ConsoleColor[] coloresAnteriores = new ConsoleColor[] { Console.BackgroundColor,Console.ForegroundColor };
            string tituloModificado = new string(' ', GetEspaciosBlancosParaCentrarTexto(titulo.Length)) + titulo;
            Console.SetCursorPosition(0, Console.CursorTop+1);
            Dictionary<char, ConsoleColor> coloresTitulo = new Dictionary<char, ConsoleColor>() { { '@', color }, { ' ', Console.BackgroundColor } };
            foreach (char letra in tituloModificado.ToUpper())
            {
                if (Letras.ContainsKey(letra))
                {
                    string[] dibujoLetra = Letras[letra];
                    WriteAt(Console.CursorLeft,Console.CursorTop, dibujoLetra[0], coloresTitulo);
                    for (int i = 1; i < dibujoLetra.Length; i++)
                    {
                        WriteAt(Console.CursorLeft - dibujoLetra[i].Length, Console.CursorTop + 1, dibujoLetra[i],coloresTitulo);
                    }
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - (dibujoLetra.Length -1));
                }
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop+9);
            Console.BackgroundColor = coloresAnteriores[0];
            Console.ForegroundColor = coloresAnteriores[1];
        }
        public static void WriteAt(int x, int y, string s, Dictionary<char,ConsoleColor> colores, Dictionary<char,ConsoleColor> texto = null)
        {
            string st = "";
            char cAct;
            int i = 0;

            while (i < s.Length)
            {
                cAct = s[i];
                st = "";

                if (colores.ContainsKey(cAct))
                {
                    Console.BackgroundColor = colores[cAct];
                    Console.ForegroundColor = (texto == null) ? colores[cAct] : texto[cAct];
                }

                while (i < s.Length && s[i] == cAct)
                {
                    st += s[i];
                    i++;
                }

                Console.SetCursorPosition(x, y);
                Console.Write(st);
                x += st.Length;
            }

            Console.ResetColor();
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
        public static void EscribirCentrado(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth/2)-(s.Length/2), Console.CursorTop);
            Console.WriteLine(s);
        }
        public static void AdvertenciaTamaño()
        {
            EscribirCentrado("".PadRight(Console.WindowWidth/2,'_'));
            Console.CursorVisible = false;
            EscribirCentrado("La interfaz está pensada para ventana grande, agrande la ventana");
            EscribirCentrado("Seleccione la 2 opcion en la barra superior de la ventana");
            EscribirCentrado("".PadRight(18, '-'));
            EscribirCentrado("| - | [esta] | x |");
            EscribirCentrado("".PadRight(18,'-'));
            EscribirCentrado("<Presione enter al terminar>");
            EscribirCentrado("".PadRight(Console.WindowWidth / 2, '_'));
            while(Console.WindowWidth < Console.LargestWindowWidth - 5)
            {
                Console.ReadKey();
            }
            Console.Clear();
        }

    }
}
