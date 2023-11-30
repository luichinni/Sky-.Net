using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    public class Localizacion
    {
        public int coordX { get; private set; } 
        public int coordY { get; private set; }
        public EnumTiposDeZona TipoZona { get; set; }
        private Cuartel cuartel = null;
        public Cuartel Cuartel { 
            get { return cuartel; } 
            set {   if (cuartel == null && value != null)
                    {
                        cuartel = value;
                        TipoZona = EnumTiposDeZona.Cuartel;
                    }
            } 
        }
        public HashSet<string> OperadoresId { get; set; }

        [JsonConstructor]
        public Localizacion(int coordX, int coordY, EnumTiposDeZona TipoZona)
        {
            this.coordX = coordX;
            this.coordY = coordY;
            OperadoresId = new HashSet<string>();
            this.TipoZona = TipoZona;
        }
        public void Salir(string id)
        {
            if (OperadoresId.Contains(id)) // si existe el operador
            {
                OperadoresId.Remove(id); // lo saca de la ubicacion
            }
        }

        public void Entrar(string id)
        {
            // si no es nulo y no está en la ubicacion
            if (!OperadoresId.Contains(id))
            {
                OperadoresId.Add(id); // lo agrega a la ubicacion
            }
        }
        public Cuartel GetCuartel()
        {
            return Cuartel;
        }
        public bool TieneCuartel()
        {
            return Cuartel != null;
        }
        public HashSet<string> GetOperadores()
        {
            return OperadoresId;
        }
        public bool HayOperadores()
        {
            return OperadoresId.Count > 0;
        }
    }
}
