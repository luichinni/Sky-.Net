using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class FabricarOperadorCmd : Comando
    {
        private Menu _menu;
        private string salida = "Terminar de Fabricar";
        public FabricarOperadorCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            List<string> opciones = new List<string>(Enum.GetNames(typeof(EnumOperadores)));
            opciones.Add(salida);
            _menu = new MenuConcreto(opciones.ToArray(),nombre);
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            bool fin = false;
            string opcion;
            while (!fin)
            {
                _menu.Mostrar();
                opcion = _menu.GetSeleccion();
                if (opcion != salida)
                {
                    Enum.TryParse(opcion, out EnumOperadores tipo);
                    Operador nuevoOperador = m.ContactarFabrica(tipo).FabricarOperador(c);
                    c.AgregarOperador(nuevoOperador);
                    ConsoleHelper.EscribirCentrado($"Operador {nuevoOperador.Id} asignado a cuartel {c.Id}");
                }
                else fin = true;
            }
            return fin;
        }
    }
}
