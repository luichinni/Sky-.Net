using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Fabricas
{
    public class FabricaK9 : Fabrica
    {
        public FabricaK9() { Tipo = EnumOperadores.K9; }
        public override Operador FabricarOperador(Cuartel c)
        {
            Bateria bateria = new Bateria();
            bateria.BateriaMax = 6500;
            bateria.BateriaActual = 6500;
            string id = GenerarId(EnumOperadores.K9,c);
            return new K9(id,bateria,c);
        }
    }
}
