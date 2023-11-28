using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class CrearCuartelCmd : Comando
    {
        public CrearCuartelCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            if (m.CantCuarteles < 3)
            {
                ConsoleHelper.EscribirCentrado("Escriba la coordenada X,Y donde quiere establecer un cuartel");
                int[] coord = GetCoordenadas(Mundo.GetInstance().MaxCoordX, Mundo.GetInstance().MaxCoordY);
                Localizacion l = m.GetLocalizacion(coord[0], coord[1]);
                if(l.IntentarEstablecerCuartel(new Cuartel())) m.CantCuarteles++;
            }
            else
            {
                ConsoleHelper.EscribirCentrado("No es posible crear más cuarteles");
            }
            

        }
        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            Console.Clear();
            ConsoleHelper.EscribirCentrado("Escriba la coordenada X,Y donde quiere establecer un cuartel");
            ConsoleHelper.EscribirCentrado($"Tamaño de la simulacion actual: 0,0 -> {Mundo.GetInstance().MaxCoordX - 1},{Mundo.GetInstance().MaxCoordY - 1}");
            string coords = Console.ReadLine();
            string[] coordsSplit = coords.Replace(" ", "").Split(','); // quita blancos innecesarios y separa por comas
            // hay que ingresar 2 coordenadas
            while (coordsSplit.Length < 2 || !SonNumeros(coordsSplit) || !ComprobarRango(coordsSplit) || !ComprobarMaximos(coordsSplit))
            {
                coords = Console.ReadLine();
                coordsSplit = coords.Replace(" ", "").Split(',');
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
