using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;

namespace SkyNet.CommandPattern.Comandos
{
    public class RecallOperadorUnicoCmd : Comando
    {
        public RecallOperadorUnicoCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, Cuartel c)
        {
            throw new NotImplementedException();
        }
    }
}
