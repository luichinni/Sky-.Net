using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class ImprimirSectorCmd : Comando
    {
        int centro;
        int maxDifX = Console.WindowWidth - 40;
        int maxDifY = Console.WindowWidth - 40;
        public ImprimirSectorCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            ConsoleColor color;
            int[] coord = GetCoordenadas(Mundo.GetInstance().MaxCoordX,Mundo.GetInstance().MaxCoordY);
            Console.Clear();
            ConsoleHelper.EscribirCentrado($"\t\tSeccion x1={coord[0]},y1={coord[1]} hasta x2={coord[2]},y={coord[3]}");
            ImprimirCoordX(coord[0], coord[2]);
            ImprimirCoordY(coord[1], coord[3]);
            for (int i = coord[0]; i <= coord[2]; i ++)
            {
                for (int j = coord[1]; j <= coord[3]; j++)
                {
                    color = ConsoleHelper.GetConsoleColor(m.GetLocalizacion(i, j).TipoZona.ToString());
                    Console.BackgroundColor = color;
                    Console.ForegroundColor = (m.GetLocalizacion(i,j).GetOperadores().Count > 0) ? ConsoleColor.White : color;
                    ConsoleHelper.WriteAt("@", centro + (i - coord[0]) * 2 + 2, j - coord[1] + 2);
                    ConsoleHelper.WriteAt("@", centro + (i - coord[0]) * 2 + 3, j - coord[1] + 2);

                    //Console.WriteLine($"POS ({i},{j}) => {mundito.GetLocalizacion(i, j).GetTipoZona()}");
                }
            }

            foreach (EnumTiposDeZona zona in Enum.GetValues(typeof(EnumTiposDeZona)))
            {
                Console.ForegroundColor = ConsoleHelper.GetConsoleColor(zona.ToString());
                Console.BackgroundColor = Console.ForegroundColor;
                ConsoleHelper.WriteAt("@", centro + (coord[2] - coord[0])*2 + 5, ((int)zona) + 2);
                Console.ResetColor();
                ConsoleHelper.WriteAt(zona.ToString(), centro + (coord[2] - coord[0]) * 2 + 6, ((int)zona) + 2);
            }
            if (coord[3] - coord[1] > Enum.GetValues(typeof(EnumTiposDeZona)).Length)  
                ConsoleHelper.WriteAt("", centro, (coord[3] - coord[1]) + 4);
            else ConsoleHelper.WriteAt("", centro, Enum.GetValues(typeof(EnumTiposDeZona)).Length + 4);
            
            return true;
        }
        private void ImprimirCoordX(int inicio, int final)
        {
            centro = Console.WindowWidth / 2;
            string cadena = "";
            for (int i = inicio; i <= final; i++)
            { /// hacemos colores intercalados para las coordenadas, de más está decir que no está pensado para coordenadas
              /// de mas de dos cifras, es mejorable pero tampoco me voy a matar en eso
                cadena += i.ToString();
            }
            centro = centro - (cadena.Length/2);
            for (int i = inicio; i <= final; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                ConsoleHelper.WriteAt(i.ToString().PadLeft(2),centro+ (i - inicio) * 2 + 2, 1);
            }
        }
        private void ImprimirCoordY(int inicio, int final)
        {
            for (int i = inicio; i <= final; i++)
            { /// hacemos colores intercalados para las coordenadas, de más está decir que no está pensado para coordenadas
              /// de mas de dos cifras, es mejorable pero tampoco me voy a matar en eso
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                ConsoleHelper.WriteAt(i.ToString().PadLeft(2), centro, (i - inicio)+2);
            }
        }
        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            Console.Clear();
            ConsoleHelper.EscribirCentrado("Ingrese separado por comas y en orden x1,y1,x2,y2 el rango de mapa que quiere ver");
            ConsoleHelper.EscribirCentrado($"Tamaño de la simulacion actual: 0,0 -> {Mundo.GetInstance().MaxCoordX-1},{Mundo.GetInstance().MaxCoordY-1}");
            ConsoleHelper.EscribirCentrado($"Nota: Maximo de diferencia x2-x1 <= {maxDifX} y y2-y1 <= {maxDifY}");
            Console.CursorLeft = Console.WindowWidth / 2 - 5;
            string coords = Console.ReadLine();
            string[] coordsSplit = coords.Replace(" ","").Split(','); // quita blancos innecesarios y separa por comas
            // hay que ingresar 4 coordenadas y respetar las diferencias
            while (coordsSplit.Length < 4 || !SonNumeros(coordsSplit) || !ComprobarRango(coordsSplit) || !ComprobarMaximos(coordsSplit))
            {
                Console.CursorLeft = Console.WindowWidth / 2 - 5;
                coords = Console.ReadLine();
                coordsSplit = coords.Replace(" ", "").Split(',');
            }

            /// las pasamos a un arreglo de integer
            int[] intCoords = new int[coordsSplit.Length];
            for (int i = 0; i < intCoords.Length; i++)
            {
                intCoords[i] = int.Parse(coordsSplit[i]);
            }
            /// Si x1 > x2, se invierten los valores
            if (intCoords[0] > intCoords[2])
            {
                int aux = intCoords[0];
                intCoords[0] = intCoords[2];
                intCoords[2] = aux;
            }
            /// Si y1 > y2, se invierten los valores
            if (intCoords[1] > intCoords[3])
            {
                int aux = intCoords[1];
                intCoords[1] = intCoords[3];
                intCoords[3] = aux;
            }
            return intCoords;
        }
        private bool ComprobarMaximos(string[] str)
        {
            bool valido = false;
            if (int.Parse(str[0]) < Mundo.GetInstance().MaxCoordX && int.Parse(str[1]) < Mundo.GetInstance().MaxCoordY && int.Parse(str[2]) < Mundo.GetInstance().MaxCoordX && int.Parse(str[3]) < Mundo.GetInstance().MaxCoordY)
                valido = true;
            return valido;
        }
        private bool ComprobarRango(string[] str)
        {
            bool valido = false;
            if (Math.Abs(int.Parse(str[0]) - int.Parse(str[2])) <= maxDifX && Math.Abs(int.Parse(str[1]) - int.Parse(str[3])) <= maxDifY) valido = true;
            return valido;
        }
        private bool SonNumeros(string[] str)
        {
            bool son = true;
            foreach (string str2 in str)
            {
                int cantNumeros = 0;
                foreach (char c in str2)
                {
                    if (c >= '0' && c <= '9') cantNumeros++;
                }
                son = son && (cantNumeros == str2.Length && str2 != "");
            }
            return son;
        }
    }
}
