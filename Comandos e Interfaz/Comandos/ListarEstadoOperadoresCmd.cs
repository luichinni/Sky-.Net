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
                string strEstado = estado.Key + ": " + estado.Value;

                Dictionary<string, bool> fallas = c.Operadores.Find(op => op.Id == estado.Key).Daños;
                string strEstado2 = strEstado + "; Daños[";
                foreach (KeyValuePair<string,bool> falla in fallas)
                {
                    if (falla.Value) strEstado2 += falla.Key+" ,";
                }

                if (strEstado2.Length > (strEstado+ "; Daños[").Length) strEstado2.Remove(strEstado2.Length - 1);
                strEstado2 += "]";

                estados.Add(strEstado2);
            }
            _menu.Opciones = estados.ToArray();
            _menu.Mostrar();
            return true;
        }
    }
}
