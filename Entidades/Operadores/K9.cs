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
        public K9(string Id, Bateria Bateria, int CoordX, int CoordY, int CuartelCoordX, int CuartelCoordY) : base(Id, Bateria, CoordX, CoordY, CuartelCoordX, CuartelCoordY)
        {
            CargaMax = 40;
            VelocidadOptima = 100;
            ZonasPeligrosas.Add(EnumTiposDeZona.Lago);
        }
    }
}
