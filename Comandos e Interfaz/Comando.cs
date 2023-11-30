using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public abstract class Comando
    {
        public string Nombre { get; protected set; }
        public string Descripcion { get; protected set; }
        public Comando(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        /// <summary>
        /// Comando recibe mundo y cuartel, es todo lo necesario para todos los comandos mas alla de si los usan o no
        /// </summary>
        /// <param name="m">Mundo como parametro (por si deja de ser singleton)</param>
        /// <param name="c">Cuartel como parametro permite trabajar con cuarteles especificos</param>
        public abstract bool Ejecutar(Mundo m, ref Cuartel c);

    }
}
