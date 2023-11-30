using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    public class UAV : Operador
    {
        [JsonConstructor]
        public UAV(string Id, Bateria Bateria, int CoordX, int CoordY, int CuartelCoordX, int CuartelCoordY) : base(Id, Bateria, CoordX, CoordY, CuartelCoordX, CuartelCoordY)
        {
            CargaMax = 5;
            VelocidadOptima = 50;
        }
    }
}
