using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public class MenuConcreto : Menu
    {
        public MenuConcreto(string[] opciones, string titulo="", char decorador = ' ') : base(opciones,titulo,decorador) { Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black; Console.Clear(); }
        public override string GetSeleccion()
        {
            string seleccion = (Opciones.Length + 1).ToString();
            while (!EsNumerico(seleccion) || int.Parse(seleccion) < 0 || int.Parse(seleccion) > Opciones.Length - 1)
            {
                ConsoleHelper.EscribirCentrado("Seleccione una opcion: ");
                Console.CursorLeft = Console.WindowWidth / 2 - 3;
                seleccion = Console.ReadLine();
            }
            return Opciones[int.Parse(seleccion)];
        }

        public override void Mostrar()
        {
            //ConsoleHelper.EscribirCentrado("".PadLeft((Console.WindowWidth/2)-Titulo.Length,Decorador)+ Titulo + "".PadRight((Console.WindowWidth / 2) - Titulo.Length, Decorador));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleHelper.WriteTitulo(Titulo, ConsoleColor.DarkGreen);
            
            for(int i = 0; i < Opciones.Length; i++)
            {
                ConsoleHelper.EscribirCentrado(i+". "+Opciones[i]);
            }
        }
        private bool EsNumerico(string str)
        {
            int cantNumeros = 0;
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9') cantNumeros++;
            }
            return cantNumeros == str.Length && str != "";
        }
    }
}
