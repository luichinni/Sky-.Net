using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    class UAV : Operador
    {
        public UAV(string id) : base(id)
        {
            bateria.InicializarBateria(4000);
            cargaMax = 5;
        }
    }
}
