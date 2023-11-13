using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public abstract class Menu
    {
        public string Titulo { get; set; }
        public string[] Opciones { get; set; }
        public char Decorador { get; set; }
        public Menu(string[] opciones, string titulo="", char decorador = ' ')
        {
            Opciones = opciones;
            if (titulo != "") Titulo = titulo;
            Decorador = decorador;
        }

        public abstract void Mostrar();
        public abstract string GetSeleccion();
    }
}
