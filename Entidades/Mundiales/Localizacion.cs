using SkyNet.Entidades.Operadores;
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
        private Cuartel cuartel;
        private Dictionary<string, Operador> operadores;

        public Localizacion(int coordX, int coordY)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            operadores = new Dictionary<string, Operador>();
        }
        public void Salir(string id)
        {
            if (operadores.ContainsKey(id)) // si existe el operador
            {
                operadores.Remove(id); // lo saca de la ubicacion
            }
        }
        public void Entrar(Operador o)
        {
            // si no es nulo y no está en la ubicacion
            if (o != null && !operadores.ContainsKey(o.Identificacion()))
            {
                operadores.Add(o.Identificacion(), o); // lo agrega a la ubicacion
            }
        }
        public bool IntentarEstablecerCuartel(Cuartel cuartel)
        {
            bool pudo = false;
            if (!TieneCuartel())
            {
                this.cuartel = cuartel;
                pudo = true;
            }
            return pudo;
        }
        public Cuartel GetCuartel()
        {
            return cuartel;
        }
        public bool TieneCuartel()
        {
            return cuartel != null;
        }
        public Dictionary<string,Operador> GetOperadores()
        {
            return operadores;
        }
        public bool HayOperadores()
        {
            return operadores.Count > 0;
        }

        public int CalcularDistancia(Localizacion otraLocalizacion)
        { // esta quizas va a una clase gps
            int distancia = 0;

            distancia += Math.Abs((coordX - otraLocalizacion.coordX));

            distancia += Math.Abs(coordY - otraLocalizacion.coordY);

            return distancia;
        }
    }
}
