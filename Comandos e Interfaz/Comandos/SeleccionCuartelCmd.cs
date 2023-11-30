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
            _menu = new MenuConcreto(ActualizarCuarteles(),nombre);
        }
        private string[] ActualizarCuarteles()
        {
            List<string> opciones = Mundo.GetInstance().GetCuarteles().Keys.ToList();
            opciones.Add(cancelar);
            return opciones.ToArray();
        }
        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            bool seleccionado = false;
            _menu.Opciones = ActualizarCuarteles();
            _menu.Mostrar();
            string op = _menu.GetSeleccion();
            if (op != cancelar)
            {
                m.GetCuarteles().TryGetValue(op, out Cuartel cu);
                if (cu != null) 
                { 
                    c = cu;
                    seleccionado = true;
                }
            }
            return seleccionado;
        }
    }
}
