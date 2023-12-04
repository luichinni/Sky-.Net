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

        public async Task MoverOperador(Operador operador, Localizacion nuevaUbicacion)
        {
            await operador.MoverAsync(nuevaUbicacion, false);
        }

        public async Task RecallOperadorUnico(Operador operador)
        {
            await operador.MoverAsync(GetUbicacion(), false);
        }

        public void PonerStandby(Operador operador)
        { 
            operador.cambiarEstado(EnumEstadoOperador.StandBy);
        }

        public void AgregarOperador(Operador robot)
        {
            Operadores.Add(robot);
        }

        public async Task EnviarAReparar(Operador robot)
        {
            Localizacion destino = robot.Gps.BuscarCercano(EnumTiposDeZona.Cuartel, robot.getUbicacion());
            await robot.MoverAsync(destino, true);
            robot.Reparar();
        }

        public void RemoverOperador(Operador robot)
        {
            Operadores.Remove(robot);
        }

        public async Task AgregarReserva(Operador robot)
        {
            await RecallOperadorUnico(robot);
            robot.cambiarEstado(EnumEstadoOperador.Reserva);
        }

        public void RemoverReserva(Operador robot)
        {
            robot.cambiarEstado(EnumEstadoOperador.Inactive);
        }




        //Métodos de Orden General

        public List<Task> TotalRecall()
        {
            List<Task> tasks = new List<Task>();

            foreach (Operador operador in Operadores)
            {
                tasks.Add(Task.Run(()=>RecallOperadorUnico(operador)));
            }

            return tasks;
        }


        public List<Task> EnviarInactivosAReciclar()
        {
            List<Task> tasks = new List<Task>();

            foreach (Operador robot in Operadores)
            {
                if (robot.Estado == EnumEstadoOperador.Inactive)
                {
                    tasks.Add(Task.Run(()=> robot.ReciclarAsync()));
                }
            }

            return tasks;
        }


        public List<Task> RealizarMantenimiento()
        {
            List<Task> tasks = new List<Task>();

            foreach (Operador robot in Operadores)
            {
                if (robot.ExisteDaño())
                {
                    tasks.Add(Task.Run(()=>EnviarAReparar(robot)));
                }
            }

            return tasks;
        }


    }
}
