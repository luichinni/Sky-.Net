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
        public M8(string Id, Bateria Bateria, int CuartelCoordX, int CuartelCoordY ) : base(Id,Bateria,CuartelCoordX,CuartelCoordY)
        {
            CargaMax = 250;
            VelocidadOptima = 200;
            ZonasPeligrosas.Add(EnumTiposDeZona.Lago);
        }
    }
}
