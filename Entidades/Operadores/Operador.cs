using SkyNet.CommandPattern.Comandos;
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

        protected Dictionary<string, bool> daños;

        protected GPS gps;

        protected List<EnumTiposDeZona> zonasPeligrosas = new List<EnumTiposDeZona>();

        public string Id { get; set; }
        public Bateria Bateria { get; set; }
        public double CargaMax { get; set; }
        public double CargaActual { get; set; }
        public double VelocidadOptima { get; set; }
        public Cuartel Cuartel { get; set; }
        public Localizacion Ubicacion { get; set; }
        public Dictionary<string, bool> Daños { get; set; }
        public GPS GPS { get; set; }
        public List<EnumTiposDeZona> ZonasPeligrosas { get; set; }

        public int PosX { get { return ubicacion.coordX; } set { ubicacion.Salir(id); ubicacion = Mundo.GetInstance().GetLocalizacion(value, PosY); ubicacion.Entrar(id); } }

        public int PosY { get { return ubicacion.coordY; } set { ubicacion.Salir(id); ubicacion = Mundo.GetInstance().GetLocalizacion(PosX, value); ubicacion.Entrar(id); } }

        public Operador(string id, Bateria bateria, Cuartel cuartel)
        {
            this.id = id;
            this.bateria = bateria;
            this.cuartel = cuartel;
            ubicacion = cuartel.GetUbicacion();
            gps = new GPS();
            zonasPeligrosas = new List<EnumTiposDeZona>() { EnumTiposDeZona.VertederoElectronico, EnumTiposDeZona.Vertedero };
            daños = new Dictionary<string, bool>
            {
                { "MotorComprometido",false },
                { "ServoAtascado",false },
                { "BateriaPerforada",false },
                { "PuertoBateriaDesconectado",false },
                { "PinturaRayada",false }
            };     
            
        }

        public void Mover (Localizacion nuevaUbicacion, bool rutaDirecta)
        {
            List<EnumTiposDeZona> zonasPeligrosasAux = new List<EnumTiposDeZona>(zonasPeligrosas); //Copia para no modificar la original

            EnumTiposDeZona[] arrayZonasPeligrosas; 

            if (rutaDirecta)
            {
                zonasPeligrosasAux.Remove(EnumTiposDeZona.VertederoElectronico);

                zonasPeligrosasAux.Remove(EnumTiposDeZona.Vertedero);
            }
            
            arrayZonasPeligrosas = zonasPeligrosasAux.ToArray();

            List<Localizacion> camino = gps.GetCamino(ubicacion, nuevaUbicacion, arrayZonasPeligrosas);

            for (int i = 1; i < camino.Count; i++)
            {
                int distancia = gps.CalcularDistancia(ubicacion,camino[i]);

                if (CalcularGastoDeBateria(distancia) <= bateria.ConsultarBateria())
                {
                    ubicacion = camino[i];

                    bateria.ConsumirBateria(CalcularGastoDeBateria(distancia));
                }
            }

        }

        public void Reciclar()
        {
            Localizacion destino = gps.BuscarCercano(EnumTiposDeZona.Vertedero, ubicacion);

            Mover(destino, true);

            Cargar();

            destino = gps.BuscarCercano(EnumTiposDeZona.SitioReciclaje, ubicacion);

            Mover(destino, true);

            Descargar();
        }


        /*public void VolverAlCuartel()  // Método Prescindible, se puede usar directamente Mover(ubicacionCuartel)
        {
            Localizacion nuevaUbicacion = cuartel.GetUbicacion();

            Mover(nuevaUbicacion);

        }*/

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

            double gastoDeBateria;

            if (!daños["BateriaPerforada"]) gastoDeBateria = tiempo * 1000;

            else gastoDeBateria = tiempo * 5000;

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

        public void Cargar()
        {
            cargaActual = cargaMax;
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

        public bool ExisteDaño()
        {
            int daño;

            Random randy = new Random();

            daño = randy.Next(1, 100);

            if (daño >= 1 & daño <= 5) return true;

            else return false;

        }

        public void ComprometerMotor()
        {
            if (daños["MotorComprometido"] != true)
            {
                velocidadOptima /= 2;
                daños["MotorComprometido"] = true;
            }
        }
        public void Dañar()
        {
            foreach (KeyValuePair<string, bool> elem in daños)
            {
                if (ExisteDaño())
                {
                    if (elem.Key == "MotorComprometido")
                    {
                        ComprometerMotor();
                    }
                    else daños[elem.Key] = true;
                }
            }
        }
    }
}
