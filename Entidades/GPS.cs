using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades
{
    public class GPS
    {
        private Mundo mundo;
        public GPS() 
        {
            mundo = Mundo.GetInstance();
        }
        public List<Localizacion> GetCamino(Localizacion origen, Localizacion destino, EnumTiposDeZona[] zonasProhibidas)
        {

            return null;
        }
        public int CalcularDistancia(Localizacion origen,Localizacion destino)
        { // este metodo lo hizo christian, yo solo lo movi de clase
            int distancia = 0;

            distancia += Math.Abs((origen.coordX - destino.coordX));

            distancia += Math.Abs(origen.coordY - destino.coordY);

            return distancia;
        }
    }
}
