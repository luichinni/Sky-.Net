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
        public UAV(string Id, Bateria Bateria, int CuartelCoordX, int CuartelCoordY) : base(Id, Bateria, CuartelCoordX, CuartelCoordY)
        {
            CargaMax = 5;
            VelocidadOptima = 50;
        }
    }
}
