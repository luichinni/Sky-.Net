using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

namespace SkyNet.CommandPattern.Comandos
{
    public class TotalRecallCmd : Comando
    {
        public TotalRecallCmd(string nombre, string descripcion) : base(nombre,descripcion)
        {
        }

        public override void Ejecutar(Mundo m,ref Cuartel c)
        {
            c.TotalRecall();
            ConsoleHelper.EscribirCentrado("Total Recall realizado con exito");
        }
    }
}
