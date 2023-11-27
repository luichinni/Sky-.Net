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
        private Dictionary<string, (Operador, EnumEstadoOperador)> operadores = new Dictionary<string, (Operador, EnumEstadoOperador)>();
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

        // Enviar Operador a ubicación en especial

        public void EnviarOperadorALocalizacion(Operador operador, Localizacion ubicacion)
        {
            operador.SetUbicacion(ubicacion);
        }

        // Volver al cuartel

        public void IndicarRetornoOperador(Operador operador)
        {
            operador.VolverAlCuartel();
        }

        /*Cambiar estado del operador a StandBy 
        Se debe agregar el estado en la creación del operador y en el constructor

        public void CambiarEstadoOperadorStandby(Operador operador)
        {
           operadores[] = EnumEstadoOperador.StandBy;
        }

        Agregar o remover operadores de la reserva, se puede hacer con el estado
         

        public void AgregarOperadorReserva(Operador EstadoOperador nuevoEstado)
        {
            EstadoOperador = nuevoEstado;
        }*/

    }
}
