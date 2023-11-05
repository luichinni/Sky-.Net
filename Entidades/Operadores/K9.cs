using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    class K9 : Operador
    {
        public K9(string id) : base(id)
        {
            bateria.InicializarBateria(6500);
            cargaMax = 40;
        }
    }
}
