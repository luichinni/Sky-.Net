using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    public class M8 : Operador
    {
        public M8(string id, Bateria bateria, Cuartel cuartel) : base(id, bateria, cuartel)
        {
            cargaMax = 250;
            velocidadOptima = 200;
            bateria.BateriaMax = 12250;
            bateria.BateriaActual = 12250;
        }
    }
}
