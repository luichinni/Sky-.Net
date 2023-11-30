using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Fabricas
{
    public class FabricaUAV : Fabrica
    {
        public FabricaUAV() { Tipo = EnumOperadores.UAV; }
        public override Operador FabricarOperador(Cuartel c)
        {
            Bateria bateria = new Bateria();
            bateria.BateriaMax = 12250;
            bateria.BateriaActual = 12250;
            string id = GenerarId(EnumOperadores.UAV, c);
            return new UAV(id, bateria, c.CoordX, c.CoordY, c.CoordX, c.CoordY);
        }
    }
}
