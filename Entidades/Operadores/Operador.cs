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
        public string Id { get; private set; }
        public Bateria Bateria { get; private set; }
        public double CargaMax { get; set; }
        public double CargaActual { get; set; }
        public double VelocidadOptima { get; set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public int CuartelCoordX { get; set; }
        public int CuartelCoordY { get; set; }
        public Dictionary<string, bool> Daños { get; set; }
        public GPS Gps { get; set; }
        public List<EnumTiposDeZona> ZonasPeligrosas { get; set; }

        private Localizacion ubicacion;
        private Localizacion ubicacionCuartel;

        public Operador(string Id, Bateria Bateria, int CuartelCoordX, int CuartelCoordY)
        {
            this.Id = Id;
            this.Bateria = Bateria;
            Localizacion ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX,CoordY);
            ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);
            ubicacionCuartel = Mundo.GetInstance().GetLocalizacion(CuartelCoordX, CuartelCoordY);
            Gps = new GPS();
            ZonasPeligrosas = new List<EnumTiposDeZona>() { EnumTiposDeZona.VertederoElectronico, EnumTiposDeZona.Vertedero };
            Daños = new Dictionary<string, bool>
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
            List<EnumTiposDeZona> zonasPeligrosasAux = new List<EnumTiposDeZona>(ZonasPeligrosas); //Copia para no modificar la original

            EnumTiposDeZona[] arrayZonasPeligrosas; 

            if (rutaDirecta)
            {
                zonasPeligrosasAux.Remove(EnumTiposDeZona.VertederoElectronico);

                zonasPeligrosasAux.Remove(EnumTiposDeZona.Vertedero);
            }
            
            arrayZonasPeligrosas = zonasPeligrosasAux.ToArray();

            List<Localizacion> camino = Gps.GetCamino(ubicacion, nuevaUbicacion, arrayZonasPeligrosas);

            for (int i = 1; i < camino.Count; i++)
            {
                int distancia = Gps.CalcularDistancia(ubicacion,camino[i]);

                if (CalcularGastoDeBateria(distancia) <= Bateria.ConsultarBateria())
                {
                    ubicacion = camino[i];

                    Bateria.ConsumirBateria(CalcularGastoDeBateria(distancia));
                }
            }

        }

        public void Reciclar()
        {
            Localizacion ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);

            Localizacion destino = Gps.BuscarCercano(EnumTiposDeZona.Vertedero, ubicacion);

            Mover(destino, true);

            Cargar();

            destino = Gps.BuscarCercano(EnumTiposDeZona.SitioReciclaje, ubicacion);

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
            Localizacion ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);

            if (ubicacion == robot.ubicacion)
            {
                if (esPosibleTransferirBateria(robot, cantBateria))
                {
                    robot.Bateria.CargarBateria(cantBateria);
                    Bateria.ConsumirBateria(cantBateria);
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
                if (robot.CargaActual + cantidad <= robot.CargaMax && CargaActual - cantidad <= 0)
                {
                    robot.CargaActual += cantidad;

                    CargaActual -= cantidad;
                }

                else Console.WriteLine("No se puede realizar la transferencia, revisar límites de carga");

                // Se envía la carga que es posible pasar? no se envía nada?

            }

            else Console.WriteLine("No se encuentran en la misma ubicación");
        }


        public double ActualizarVelocidad()
        {
            double porcentajeCarga = CargaActual / CargaMax;

            double porcentajeAReducir = porcentajeCarga * 0.05 / 0.1;

            double nuevaVelocidad = VelocidadOptima - VelocidadOptima * porcentajeAReducir;

            return nuevaVelocidad;

        }

        public double CalcularGastoDeBateria(int distancia)
        {
            double velocidad = ActualizarVelocidad();

            double tiempo = distancia / velocidad;

            double gastoDeBateria;

            if (!Daños["BateriaPerforada"]) gastoDeBateria = tiempo * 1000;

            else gastoDeBateria = tiempo * 5000;

            return gastoDeBateria;
        }

        public bool esPosibleTransferirBateria(Operador robot, double cantBateria)
        {
            double bateriaEntrega = GetBateria();

            double bateriaRecibe = robot.GetBateria();

            double bateriaMaxRecibe = robot.Bateria.GetBateriaMax();

            if (bateriaEntrega - cantBateria >= 0 && bateriaRecibe + cantBateria <= bateriaMaxRecibe) return true;

            else return false;
        }

        public void RecargarBateria()
        {
            Bateria.LlenarBateria();
        }

        public void ConsumirEnergia(double cantBateria)
        {
            Bateria.ConsumirBateria(cantBateria);
        }

        public void Cargar()
        {
            CargaActual = CargaMax;
        }

        public void Descargar()
        {
            CargaActual = 0;

            // Donde se almacena lo que se descarga?

        }

        public Localizacion getUbicacion()
        {
            return ubicacion;

        }

        public string GetId() { return Id; }

        public double GetBateria()
        {
            return Bateria.ConsultarBateria();
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
            if (Daños["MotorComprometido"] != true)
            {
                VelocidadOptima /= 2;
                Daños["MotorComprometido"] = true;
            }
        }
        public void Dañar()
        {
            foreach (KeyValuePair<string, bool> elem in Daños)
            {
                if (ExisteDaño())
                {
                    if (elem.Key == "MotorComprometido")
                    {
                        ComprometerMotor();
                    }
                    else Daños[elem.Key] = true;
                }
            }
        }
    }
}
