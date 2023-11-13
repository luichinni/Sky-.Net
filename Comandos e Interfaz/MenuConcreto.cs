using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public class MenuConcreto : Menu
    {
        public MenuConcreto(string[] opciones, string titulo="", char decorador = ' ') : base(opciones,titulo,decorador){ }
        public override string GetSeleccion()
        {
            string seleccion = (Opciones.Length + 1).ToString();
            while (!EsNumerico(seleccion) || int.Parse(seleccion) < 0 || int.Parse(seleccion) > Opciones.Length - 1)
            {
                Console.WriteLine("Seleccione una opcion: ");
                seleccion = Console.ReadLine();
            }
            return Opciones[int.Parse(seleccion)];
        }

        public override void Mostrar()
        {
            Console.WriteLine(Titulo.PadLeft(Titulo.Length+6,Decorador).PadRight(Titulo.Length+12,Decorador));
            for(int i = 0; i < Opciones.Length; i++)
            {
                Console.WriteLine(i+". "+Opciones[i]);
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
