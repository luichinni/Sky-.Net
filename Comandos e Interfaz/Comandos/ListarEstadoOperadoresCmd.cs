using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;

namespace SkyNet.CommandPattern.Comandos
{
    public class ListarEstadoOperadoresCmd : Comando
    {
        Menu _menu;
        public ListarEstadoOperadoresCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            string[] opciones = new string[] { };
            _menu = new MenuConcreto(opciones,nombre);
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            List<string> estados = new List<string>();
            foreach(KeyValuePair<string,EnumEstadoOperador> estado in c.ListarEstadoOperadores())
            {
                estados.Add(estado.Key + ": " + estado.Value);
            }
            _menu.Opciones = estados.ToArray();
            _menu.Mostrar();
            return true;
        }
    }
}
