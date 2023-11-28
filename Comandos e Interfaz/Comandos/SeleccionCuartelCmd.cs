using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class SeleccionCuartelCmd : Comando
    {
        private Menu _menu;
        private string cancelar = "Cancelar";
        public SeleccionCuartelCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            List<string> opciones = Mundo.GetInstance().GetCuarteles().Keys.ToList();
            opciones.Add(cancelar);
            _menu = new MenuConcreto(opciones.ToArray(),nombre);
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            _menu.Mostrar();
            string op = _menu.GetSeleccion();
            if (op != cancelar)
            {
                m.GetCuarteles().TryGetValue(op, out Cuartel cu);
                if (cu != null) c = cu;
            }
        }
    }
}
