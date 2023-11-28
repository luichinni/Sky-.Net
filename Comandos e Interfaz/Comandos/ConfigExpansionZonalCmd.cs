using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class ConfigExpansionZonalCmd : ConfigZonalCmd
    {
        public ConfigExpansionZonalCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            bool fin = false;
            string seleccion;
            while (!fin)
            {
                menu.Mostrar();
                seleccion = menu.GetSeleccion();
                if (seleccion == salida) fin = true;
                else
                {
                    int valorViejo = m.ExpansionZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)];
                    ConsoleHelper.EscribirCentrado($"Valor actual: {valorViejo}, ingrese el nuevo valor deseado para {seleccion}");
                    Console.CursorLeft = Console.WindowWidth / 2 - 3;
                    string valorNuevo = Console.ReadLine();
                    while (!EsNumerico(valorNuevo)) { Console.CursorLeft = Console.WindowWidth / 2 - 3; valorNuevo = Console.ReadLine(); }
                    m.ExpansionZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)] = int.Parse(valorNuevo);
                }
            }
        }
    }
}
