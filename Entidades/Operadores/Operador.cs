using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    abstract class Operador
    {
        protected string id;

        protected Bateria bateria;

        protected double cargaMax, cargaActual, velActual;

        protected Localizacion ubicacion;

        public Operador(string id)
        {
            bateria = new Bateria();
            this.id = id;
            velActual = 300;
            ubicacion = new Localizacion(0, 0);   // Provisorio, se instancia en otro lado

        }

        public void Mover(Localizacion nuevaUbicacion)
        {
            int distancia = ubicacion.CalcularDistancia(nuevaUbicacion);

            if (CalcularGastoDeBateria(distancia) <= bateria.ConsultarBateria())
            {
                ubicacion = nuevaUbicacion;

                bateria.ConsumirBateria(CalcularGastoDeBateria(distancia));

                //Agregar a lista de nueva ubicacion y quitar de lista de ubicación vieja

            }
            else
            {
                Console.WriteLine("No alcanza la bateria para llegar a esa ubicación");

                //Qué hacer en caso de no alcanzar la bateria (quedarse? ir hasta donde llegue?)
            }
        }

        public Localizacion getUbicacion()
        {
            return ubicacion;

        }

        public void RetornarCuartel()
        {
            // Ubicación del cuartel?
            //Actualizar Batería
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

              // Si la transferencia sobrepasa la carga Maxima se transfiere hasta que llegue a la carga maxima?
              // Si quien entrega la bateria tiene menos de la que se quiere transferir se entrega hasta que quede en 0?
              // o no se transfiere nada? 
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

                    robot.ActualizarVelocidad();

                    cargaActual -= cantidad;

                    ActualizarVelocidad();
                }

                else Console.WriteLine("No se puede realizar la transferencia, revisar límites de carga");

                // Se envía la carga que es posible pasar? no se envía nada?

            }

            else Console.WriteLine("No se encuentran en la misma ubicación");
        }

        public void Descargar()
        {
            // Considerar que esté en el cuartel. Donde se almacena lo que se descarga?

            cargaActual = 0;
            ActualizarVelocidad();
        }

        public void RecargarBateria()
        {
            bateria.CargarBateriaMax();
        }

        public void ConsumirEnergia(double cantBateria)
        {
            bateria.ConsumirBateria(cantBateria);
        }

        public double CalcularGastoDeBateria(int distancia)
        {
            // calculos

            return 0;
        }

        public bool esPosibleTransferirBateria(Operador robot, double cantBateria)
        {
            double bateriaEntrega = GetBateria();

            double bateriaRecibe = robot.GetBateria();

            double bateriaMaxRecibe = robot.bateria.GetBateriaMax();

            if (bateriaEntrega - cantBateria >= 0 && bateriaRecibe + cantBateria <= bateriaMaxRecibe) return true;

            else return false;
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

        public int CalcularDistancia(Localizacion nuevaUbicacion)
        {
            int distancia = ubicacion.CalcularDistancia(nuevaUbicacion);

            return distancia;
        }

       

        public string GetId() { return id; }

        public double GetBateria()
        {
            return bateria.ConsultarBateria();
        }

        public void ActualizarVelocidad()
        {
            // Reducir 5% por cada 10% de carga

        }
    }
}
