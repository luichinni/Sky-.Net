using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Fabricas
{
    public class FabricaM8 : Fabrica
    {
        public FabricaM8() { Tipo = EnumOperadores.M8; }
        public override Operador FabricarOperador(Cuartel c)
        {
            Bateria bateria = new Bateria();
            bateria.BateriaMax = 12250;
            bateria.BateriaActual = 12250;
            string id = GenerarId(EnumOperadores.M8, c);
            return new M8(id, bateria, c);
        }
    }
}
