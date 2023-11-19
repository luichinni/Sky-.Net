using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    public class K9 : Operador
    {
        public K9(string id, Bateria bateria, Cuartel cuartel) : base(id, bateria, cuartel)
        {
            cargaMax = 40;
            velocidadOptima = 100;
            bateria.BateriaMax = 6500;
            bateria.BateriaActual = 6500;
        }
    }
}
