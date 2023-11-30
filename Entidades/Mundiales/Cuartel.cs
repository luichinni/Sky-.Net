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
        private Localizacion ubicacionCuartel;

        public string Id { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public List<Operador> Operadores { get; private set; }


        public Cuartel(string Id, int CoordX, int CoordY, List<Operador> Operadores = null)
        { 
            this.Id = Id;
            this.CoordX = CoordX;
            this.CoordY = CoordY;
            this.Operadores = (Operadores!=null) ? Operadores : new List<Operador>();
            //ubicacionCuartel = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);
        }
        /* no necesario, tenemos la propiedad
        public string Identificacion()
        {
            return Id;
        }*/

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

            foreach(Operador robot in Operadores)
            {
                estadoOperadores.Add(robot.Id, robot.Estado);
            }

            return estadoOperadores;
        }

        public Dictionary<string, EnumEstadoOperador> ListarEstadoOperadoresUbicacion(Localizacion ubicacionAInspeccionar)
        {
            Dictionary<string, EnumEstadoOperador> estadoOperadores = new Dictionary<string, EnumEstadoOperador>();

            foreach (Operador robot in Operadores)
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
            foreach (Operador operador in Operadores)
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
            Operadores.Add(robot);
        }

        public void RemoverOperador(Operador robot)
        {
            Operadores.Remove(robot);
        }

        public void RemoverReserva ()
        {
            Operadores.Clear();
        }

        public void EnviarInactivosAReciclar()
        {
            foreach (Operador robot in Operadores)
            {
                if (robot.Estado == EnumEstadoOperador.Inactive)
                {
                    robot.Reciclar();
                }
            }
        }

        public void RealizarMantenimiento()
        {
            foreach (Operador robot in Operadores)
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
