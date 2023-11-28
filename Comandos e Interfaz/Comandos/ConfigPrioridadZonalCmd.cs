using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class ConfigPrioridadZonalCmd : ConfigZonalCmd
    {
        public ConfigPrioridadZonalCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            bool fin = false;
            string seleccion;
            while (!fin)
            {
                menu.Mostrar();
                ConsoleHelper.EscribirCentrado("Nota: las prioridades se actualizarán para evitar tener 2 iguales");
                seleccion = menu.GetSeleccion();
                if (seleccion == salida) fin = true;
                else
                {
                    int valorViejo = m.PrioridadZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)];
                    ConsoleHelper.EscribirCentrado($"Valor actual: {valorViejo}, ingrese el nuevo valor deseado para {seleccion}");
                    Console.CursorLeft = Console.WindowWidth / 2 - 3;
                    string valorNuevo = Console.ReadLine();
                    while (!EsNumerico(valorNuevo)) { valorNuevo = Console.ReadLine(); Console.CursorLeft = Console.WindowWidth / 2 - 3; }
                    for (int i = 0; i < m.PrioridadZonal.Length; i++)
                    {
                        if (m.PrioridadZonal[i] == int.Parse(valorNuevo)) m.PrioridadZonal[i] = valorViejo;
                    }
                    m.PrioridadZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)] = int.Parse(valorNuevo);
                }
            }
        }
    }
}
