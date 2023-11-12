using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    public abstract class Operador
    {
        protected string id;

        protected Bateria bateria;

        protected double cargaMax, cargaActual, velocidadOptima;

        protected Cuartel cuartel;

        protected Localizacion ubicacion;

        public Operador(string id, Bateria bateria, Cuartel cuartel)
        {
            this.id = id;
            this.bateria = bateria;
            this.cuartel = cuartel;
            velocidadOptima = 300;
            ubicacion = cuartel.GetUbicacion();
        }

        public void Mover(Localizacion nuevaUbicacion)
        {
            int distancia = ubicacion.CalcularDistancia(nuevaUbicacion);

            if (CalcularGastoDeBateria(distancia) <= bateria.ConsultarBateria())
            {
                ubicacion = nuevaUbicacion;

                bateria.ConsumirBateria(CalcularGastoDeBateria(distancia));
            }

            else
            {
                Console.WriteLine("No alcanza la bateria para llegar a esa ubicación");

                //Qué hacer en caso de no alcanzar la bateria (quedarse? ir hasta donde llegue?)
            }
        }

        public void VolverAlCuartel()  // Método Prescindible, se puede usar directamente Mover(ubicacionCuartel)
        {
            Localizacion nuevaUbicacion = cuartel.GetUbicacion();

            Mover(nuevaUbicacion);

        }

        public void TransferirBateria(Operador robot, double cantBateria)
        {
            if (ubicacion == robot.ubicacion)
            {
                if (esPosibleTransferirBateria(robot, cantBateria))
                {
                    robot.bateria.CargarBateria(cantBateria);
                    bateria.ConsumirBateria(cantBateria);
                }

                else Console.WriteLine("No se puede realizar la transferencia, revisar límites de batería");

                // Se envía la batería que es posible pasar? No se envía nada?
            }

            else Console.WriteLine("No se puede realizar la transferencia por que no están en la misma ubicación");


        }

        public void transferirCargaA(Operador robot, double cantidad)
        {
            if (ubicacion == robot.ubicacion)
            {
                if (robot.cargaActual + cantidad <= robot.cargaMax && cargaActual - cantidad <= 0)
                {
                    robot.cargaActual += cantidad;

                    cargaActual -= cantidad;
                }

                else Console.WriteLine("No se puede realizar la transferencia, revisar límites de carga");

                // Se envía la carga que es posible pasar? no se envía nada?

            }

            else Console.WriteLine("No se encuentran en la misma ubicación");
        }


        public double ActualizarVelocidad()
        {
            double porcentajeCarga = cargaActual / cargaMax;

            double porcentajeAReducir = porcentajeCarga * 0.05 / 0.1;

            double nuevaVelocidad = velocidadOptima - velocidadOptima * porcentajeAReducir;

            return nuevaVelocidad;

        }


        public double CalcularGastoDeBateria(int distancia)
        {
            double velocidad = ActualizarVelocidad();

            double tiempo = distancia / velocidad;

            double gastoDeBateria = tiempo * 1000;

            return gastoDeBateria;
        }

        public bool esPosibleTransferirBateria(Operador robot, double cantBateria)
        {
            double bateriaEntrega = GetBateria();

            double bateriaRecibe = robot.GetBateria();

            double bateriaMaxRecibe = robot.bateria.GetBateriaMax();

            if (bateriaEntrega - cantBateria >= 0 && bateriaRecibe + cantBateria <= bateriaMaxRecibe) return true;

            else return false;
        }

        public void RecargarBateria()
        {
            bateria.LlenarBateria();
        }

        public void ConsumirEnergia(double cantBateria)
        {
            bateria.ConsumirBateria(cantBateria);
        }

        public void Descargar()
        {
            cargaActual = 0;

            // Donde se almacena lo que se descarga?

        }

        public Localizacion getUbicacion()
        {
            return ubicacion;

        }

        public string GetId() { return id; }

        public double GetBateria()
        {
            return bateria.ConsultarBateria();
        }

        public string Identificacion()
        {
            string alfanumerico = "ABCDEFGHIJKLMNÑOPQRSTUV1234567890";

            string id = "";

            Random randy = new Random();

            for (int i = 0; i < 6; i++)
            {

                id += alfanumerico[randy.Next(1, 33)]; ;

            }

            return id;
        }
    }
}
