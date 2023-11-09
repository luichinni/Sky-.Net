using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    class UAV : Operador
    {
        public UAV(string id, Bateria bateria, Cuartel cuartel) : base(id, bateria, cuartel)
        {
            cargaMax = 5;
        }
    }
}
