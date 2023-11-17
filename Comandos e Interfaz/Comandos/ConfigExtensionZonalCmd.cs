using SkyNet.CommandPattern.Comandos;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class ConfigExtensionZonalCmd : ConfigZonalCmd
    {
        public ConfigExtensionZonalCmd(string nombre, string descripcion) : base(nombre, descripcion)
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
                    int valorViejo = m.ExtensionZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)];
                    Console.WriteLine($"Valor actual: {valorViejo}, ingrese el nuevo valor deseado para {seleccion}");
                    string valorNuevo = Console.ReadLine();
                    while (!EsNumerico(valorNuevo)) valorNuevo = Console.ReadLine();
                    m.ExtensionZonal[(int)Enum.Parse(typeof(EnumTiposDeZona), seleccion)] = int.Parse(valorNuevo);
                }
            }
        }
    }
}
