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
                int[] coord = GetCoordenadas(Mundo.GetInstance().MaxCoordX, Mundo.GetInstance().MaxCoordY);
                Localizacion l = m.GetLocalizacion(coord[0], coord[1]);
                string nId = GenerarId(4);
                while (m.GetCuarteles().Keys.Contains(nId)) nId = GenerarId(4);
                Cuartel nuevoCuartel = new Cuartel(nId, coord[0], coord[1]);
                l.Cuartel = nuevoCuartel;
                if (l.Cuartel == nuevoCuartel)
                {
                    m.CantCuarteles++;
                    m.RegistrarCuartel(nuevoCuartel);
                }
            }
            else
            {
                ConsoleHelper.EscribirCentrado("No es posible crear más cuarteles");
            }
        }
        private string GenerarId(int cantidad)
        {
            Random rnd = new Random();
            string str = "";
            while (str.Length < cantidad)
            {
                if (rnd.NextDouble() > 0.5)
                    str = str + (char)rnd.Next(65, 91); // letras mayus
                else
                    str = str + (char)rnd.Next(48, 58); // numeros
            }
            return str;
        }
        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            Console.Clear();
            ConsoleHelper.EscribirCentrado("Escriba la coordenada X,Y donde quiere establecer un cuartel");
            ConsoleHelper.EscribirCentrado($"Tamaño de la simulacion actual: 0,0 -> {Mundo.GetInstance().MaxCoordX - 1},{Mundo.GetInstance().MaxCoordY - 1}");
            Console.CursorLeft = Console.WindowWidth / 2 - 4;
            
            string coords = Console.ReadLine();
            string[] coordsSplit = coords.Replace(" ", "").Split(','); // quita blancos innecesarios y separa por comas
            // hay que ingresar 2 coordenadas
            
            while (coordsSplit.Length < 2 || !SonNumeros(coordsSplit) || !ComprobarMaximos(coordsSplit))
            {
                coords = Console.ReadLine();
                coordsSplit = coords.Replace(" ", "").Split(',');
            }
            
            int[] intCoords = new int[coordsSplit.Length];
            for (int i = 0; i < intCoords.Length; i++)
            {
                intCoords[i] = int.Parse(coordsSplit[i]);
            }
            return intCoords;
        }
        private bool ComprobarMaximos(string[] str)
        {
            bool valido = false;
            if (int.Parse(str[0]) < Mundo.GetInstance().MaxCoordX && int.Parse(str[1]) < Mundo.GetInstance().MaxCoordY)
                valido = true;
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
