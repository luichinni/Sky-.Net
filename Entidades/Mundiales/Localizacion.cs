using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    public class Localizacion
    {
        public string Pos { get; private set; }
        public int coordX { get; private set; } 
        public int coordY { get; private set; }
        private string cuartelId = null;
        private HashSet<string> operadores;
        private EnumTiposDeZona tipoZona;

        public Localizacion(int coordX, int coordY, EnumTiposDeZona tipoZona)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            Pos = $"x{coordX}y{coordY}";
            operadores = new HashSet<string>();
            this.tipoZona = tipoZona;
        }
        public EnumTiposDeZona GetTipoZona()
        {
            return tipoZona;
        }
        public void SetTipoZona(EnumTiposDeZona tipoZona)
        {
            this.tipoZona = tipoZona;
        }
        public void Salir(string id)
        {
            if (operadores.Contains(id)) // si existe el operador
            {
                operadores.Remove(id); // lo saca de la ubicacion
            }
        }
        public void Entrar(Operador o)
        {
            // si no es nulo y no está en la ubicacion
            if (o != null && !operadores.Contains(o.Identificacion()))
            {
                operadores.Add(o.Identificacion()); // lo agrega a la ubicacion
            }
        }
        public bool IntentarEstablecerCuartel(Cuartel cuartel)
        {
            bool pudo = false;
            if (!TieneCuartel())
            {
                this.cuartelId = cuartel.Identificacion();
                this.tipoZona = EnumTiposDeZona.Cuartel;
                pudo = true;
            }
            return pudo;
        }
        public Cuartel GetCuartel()
        {
            return (TieneCuartel()) ? Mundo.GetInstance().GetCuartel(coordX,coordY) : null;
        }
        public bool TieneCuartel()
        {
            return cuartelId != null;
        }
        public HashSet<string> GetOperadores()
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
