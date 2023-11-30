using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    internal class RealizarMantenimientoCmd : Comando
    {
        public RealizarMantenimientoCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            c.RealizarMantenimiento();
            return true;
        }
    }
}
