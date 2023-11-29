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
        private int coordX;
        private int coordY;
        private List<Operador> operadores;
        private Localizacion ubicacionCuartel;


        public string Id { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public List<Operador> Operadores { get; set; }
        public Localizacion UbicacionCuartel { get; set; }


        public Cuartel(Localizacion ubicacionCuartel)
        { 
            coordX = ubicacionCuartel.coordX;
            coordY = ubicacionCuartel.coordY;
            operadores = new List<Operador>();
            ubicacionCuartel = Mundo.GetInstance().GetLocalizacion(coordX, coordY);
        }
        public string Identificacion()
        {
            return id;
        }

        public Localizacion GetUbicacion()
        {
            return ubicacionCuartel;
        }

        // Listado de Operadores por localización 

        /*public List<Operador> ListarOperadoresPorUbicacion(Localizacion ubicacion)
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
        }*/

        public Dictionary<string, EnumEstadoOperador> ListarEstadoOperadores()
        {
            Dictionary<string, EnumEstadoOperador> estadoOperadores = new Dictionary<string,EnumEstadoOperador>();

            foreach(Operador robot in operadores)
            {
                estadoOperadores.Add(robot.Id, robot.Estado);
            }

            return estadoOperadores;
        }

        public Dictionary<string, EnumEstadoOperador> ListarEstadoOperadoresUbicacion(Localizacion ubicacionAInspeccionar)
        {
            Dictionary<string, EnumEstadoOperador> estadoOperadores = new Dictionary<string, EnumEstadoOperador>();

            foreach (Operador robot in operadores)
            {
                if(robot.getUbicacion()==ubicacionAInspeccionar)
                {
                    estadoOperadores.Add(robot.Id, robot.Estado);
                }
            }

            return estadoOperadores;
        }

        public void TotalRecall()
        {
            foreach (Operador operador in operadores)
            {
                RecallOperadorUnico(operador);
            }
        }


        public void MoverOperador(Operador operador, Localizacion nuevaUbicacion)
        {
            operador.Mover(nuevaUbicacion, false);
        }

        public void RecallOperadorUnico(Operador operador)
        {
            operador.Mover(ubicacionCuartel, false);
        }

        public void PonerStandby(Operador operador)
        { 
            operador.cambiarEstado(EnumEstadoOperador.StandBy);
        }

        public void AgregarOperador(Operador robot)
        {
            operadores.Add(robot);
        }

        public void RemoverOperador(Operador robot)
        {
            operadores.Remove(robot);
        }

        public void RemoverReserva ()
        {
            operadores.Clear();
        }

        public void EnviarInactivosAReciclar()
        {
            foreach (Operador robot in operadores)
            {
                if (robot.Estado == EnumEstadoOperador.Inactive)
                {
                    robot.Reciclar();
                }
            }
        }

        public void RealizarMantenimiento()
        {
            foreach (Operador robot in operadores)
            {
                if(robot.ExisteDaño())
                {
                    robot.Gps.BuscarCercano(EnumTiposDeZona.Cuartel, robot.getUbicacion());
                    robot.Reparar();
                }
            }
        }
    }
}
