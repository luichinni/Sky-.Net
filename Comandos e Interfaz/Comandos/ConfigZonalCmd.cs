using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public abstract class ConfigZonalCmd : Comando
    {
        protected Menu menu;
        protected string salida = "Terminar Configuracion";
        public ConfigZonalCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            List<string> opAux = new List<string>(Enum.GetNames(typeof(EnumTiposDeZona)));
            opAux.Add(salida);
            string[] opciones = opAux.ToArray();
            menu = new MenuConcreto(opciones,nombre);
        }
        protected bool EsNumerico(string str)
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
