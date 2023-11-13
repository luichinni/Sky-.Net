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
        public ImprimirSectorCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, Cuartel c)
        {
            ConsoleColor color;
            int[] coord = GetCoordenadas(Mundo.MaxCoordX,Mundo.MaxCoordY);
            Console.Clear();
            Console.WriteLine($"\t\tSeccion x1={coord[0]},y1={coord[1]} hasta x2={coord[2]},y={coord[3]}");
            for (int i = coord[0]; i <= coord[2]; i ++)
            {
                for (int j = coord[1]; j <= coord[3]; j++)
                {
                    color = ConsoleHelper.GetConsoleColor(m.GetLocalizacion(i, j).GetTipoZona().ToString());
                    Console.BackgroundColor = color;
                    Console.ForegroundColor = color;
                    ConsoleHelper.WriteAt("@", (i - coord[0]) * 2 + 2, j - coord[1] + 2);
                    ConsoleHelper.WriteAt("@", (i - coord[0]) * 2 + 3, j - coord[1] + 2);

                    //Console.WriteLine($"POS ({i},{j}) => {mundito.GetLocalizacion(i, j).GetTipoZona()}");
                }
            }

            foreach (EnumTiposDeZona zona in Enum.GetValues(typeof(EnumTiposDeZona)))
            {
                Console.ForegroundColor = ConsoleHelper.GetConsoleColor(zona.ToString());
                Console.BackgroundColor = Console.ForegroundColor;
                ConsoleHelper.WriteAt("@", (coord[2] - coord[0])*2 + 5, ((int)zona) + 2);
                Console.ResetColor();
                ConsoleHelper.WriteAt(zona.ToString(), (coord[2] - coord[0]) * 2 + 6, ((int)zona) + 2);
            }
            ConsoleHelper.WriteAt("", 0, (coord[3] - coord[1]) + 4);
        }
        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            Console.Clear();
            Console.WriteLine("Ingrese separado por comas y en orden x1,y1,x2,y2 el rango de mapa que quiere ver");
            Console.WriteLine($"Tamaño de la simulacion actual: 0,0 -> {Mundo.MaxCoordX-1},{Mundo.MaxCoordY-1}");
            Console.WriteLine("Nota: Maximo de diferencia x1-x2 es de 40 y de y1-y2 es de 25");
            string coords = Console.ReadLine();
            string[] coordsSplit = coords.Replace(" ","").Split(','); // quita blancos innecesarios y separa por comas
            // hay que ingresar 4 coordenadas y respetar las diferencias
            while (coordsSplit.Length < 4 || !SonNumeros(coordsSplit) || !ComprobarRango(coordsSplit) || !ComprobarMaximos(coordsSplit))
            {
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
            if (int.Parse(str[0]) < Mundo.MaxCoordX && int.Parse(str[1]) < Mundo.MaxCoordY && int.Parse(str[2]) < Mundo.MaxCoordX && int.Parse(str[3]) < Mundo.MaxCoordY)
                valido = true;
            return valido;
        }
        private bool ComprobarRango(string[] str)
        {
            bool valido = false;
            if (Math.Abs(int.Parse(str[0]) - int.Parse(str[2])) <= 40 || Math.Abs(int.Parse(str[1]) - int.Parse(str[3])) <= 25) valido = true;
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
