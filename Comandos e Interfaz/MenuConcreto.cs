using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public class MenuConcreto : Menu
    {
        public MenuConcreto(string[] opciones, string titulo="") : base(opciones,titulo){ }
        public override string GetSeleccion()
        {
            string seleccion = (opciones.Length + 1).ToString();
            while (!EsNumerico(seleccion) || int.Parse(seleccion) < 0 || int.Parse(seleccion) > opciones.Length - 1)
            {
                Console.WriteLine("Seleccione una opcion: ");
                seleccion = Console.ReadLine();
            }
            return opciones[int.Parse(seleccion)];
        }

        public override void Mostrar()
        {
            Console.WriteLine(titulo);
            for(int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine(i+". "+opciones[i]);
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
