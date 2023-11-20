using System;
using SkyNet.Entidades.Operadores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern.Comandos;

namespace SkyNet.Entidades.Mundiales
{
    public class Cuartel
    {
        private string id;
        private Localizacion ubicacion;
        private Dictionary<string, Operador> operadores = new Dictionary<string, Operador>();


        public string Identificacion()
        {
            return id;
        }

        public Localizacion GetUbicacion()
        {
            return ubicacion;
        }

        // Listado de Operadores por localización 

        public List<Operador> ListarOperadoresPorUbicacion(Localizacion ubicacion)
        {
            List<Operador> operadoresEnUbicacion = new List<Operador>();

            foreach (Operador operador in operadores.Values)
            {
                if (operador.getUbicacion().Equals(ubicacion))
                {
                    operadoresEnUbicacion.Add(operador);
                }
            }
            return operadoresEnUbicacion;
        }

        // Hacer un Total Recall (llamado y retorno)
        public void TotalRecall()
        {
            foreach (Operador operador in operadores.Values)
            {
                operador.VolverAlCuartel();
            }
        }


    }
}
