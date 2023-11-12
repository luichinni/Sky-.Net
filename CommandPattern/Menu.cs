using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public abstract class Menu
    {
        protected string titulo;
        protected string[] opciones;
        public Menu(string[] opciones, string titulo="")
        {
            this.opciones = opciones;
            if (titulo != "") this.titulo = titulo;
        }

        public abstract void Mostrar();
        public abstract string GetSeleccion();
    }
}
