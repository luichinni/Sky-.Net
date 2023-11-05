using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    class Localizacion
    {
        private int coordX, coordY;


        public Localizacion(int coordX, int coordY)
        {
            this.coordX = coordX;
            this.coordY = coordY;

        }

        public int CalcularDistancia(Localizacion otraLocalizacion)
        {
            int distancia = 0;

            distancia += Math.Abs((coordX - otraLocalizacion.coordX));

            distancia += Math.Abs(coordY - otraLocalizacion.coordY);

            return distancia;
        }
    }
}
