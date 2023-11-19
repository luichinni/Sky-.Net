using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    public class UAV : Operador
    {
        public UAV(string id, Bateria bateria, Cuartel cuartel) : base(id, bateria, cuartel)
        {
            cargaMax = 5;
            velocidadOptima = 50;
            bateria.BateriaMax = 4000;
            bateria.BateriaActual = 4000;
        }
    }
}
