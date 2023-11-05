using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    class M8 : Operador
    {
        public M8(string id) : base(id)
        {
            bateria.InicializarBateria(12250);
            cargaMax = 250;
        }
    }
}
