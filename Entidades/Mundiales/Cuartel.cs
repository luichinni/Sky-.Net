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
        }

        public Localizacion GetUbicacion()
        {
            return Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);
        }


        //Métodos que retornan listas

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

        /*
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
        */


        // Métodos de Orden Individual

        public void MoverOperador(Operador operador, Localizacion nuevaUbicacion)
        {
            operador.Mover(nuevaUbicacion, false);
        }

        public void RecallOperadorUnico(Operador operador)
        {
            operador.Mover(GetUbicacion(), false);
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

        public void AgregarReserva(Operador robot)
        {
            RecallOperadorUnico(robot);
            robot.cambiarEstado(EnumEstadoOperador.Reserva);
        }

        public void RemoverReserva(Operador robot)
        {
            robot.cambiarEstado(EnumEstadoOperador.Inactive);
        }



        //Métodos de Orden General

        public void TotalRecall()
        {
            foreach (Operador operador in Operadores)
            {
                RecallOperadorUnico(operador);
            }
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
