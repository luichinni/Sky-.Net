using SkyNet.CommandPattern.Comandos;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Operadores
{
    [JsonDerivedType(typeof(UAV), typeDiscriminator: "UAV")]
    [JsonDerivedType(typeof(K9), typeDiscriminator: "K9")]
    [JsonDerivedType(typeof(M8), typeDiscriminator: "M8")]
    public abstract class Operador
    {
        public string Id { get; private set; }
        public Bateria Bateria { get; private set; }
        public EnumEstadoOperador Estado { get; private set; }
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
        //private Localizacion ubicacionCuartel;
        [JsonConstructor]
        public Operador(string Id, Bateria Bateria, int CoordX, int CoordY, int CuartelCoordX, int CuartelCoordY)
        {
            this.Id = Id;
            this.Bateria = Bateria;
            this.CoordX = CoordX;
            this.CoordY = CoordY;
            this.CuartelCoordX = CuartelCoordX;
            this.CuartelCoordY = CuartelCoordY;
            Estado = EnumEstadoOperador.Inactive;
            //ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);
            //ubicacionCuartel = Mundo.GetInstance().GetLocalizacion(CuartelCoordX, CuartelCoordY);
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
            //ubicacion.Entrar(Id);

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

                double tiempo = CalcularTiempoDeViaje(distancia);

                if (CalcularGastoDeBateria(tiempo) <= Bateria.ConsultarBateria())
                {
                    ubicacion = camino[i];

                    Bateria.ConsumirBateria(CalcularGastoDeBateria(tiempo));
                }
            }

        }

        public void Reciclar()
        {
            Localizacion destino = Gps.BuscarCercano(EnumTiposDeZona.Vertedero, ubicacion);

            Mover(destino, true);

            Cargar();

            destino = Gps.BuscarCercano(EnumTiposDeZona.SitioReciclaje, ubicacion);

            Mover(destino, true);

            Descargar();
        }

        public void TransferirBateria(Operador robot, double cantidad)
        {

            if (!Daños["BateriaPerforada"])
            {
                if (ubicacion != robot.ubicacion) Mover(robot.ubicacion, false);

                if (Bateria.BateriaActual - cantidad >= 0 && robot.Bateria.BateriaActual + cantidad <= robot.Bateria.BateriaMax)
                {
                    ConsumirEnergia(cantidad);
                    robot.RecargarBateria(cantidad);
                }
                else
                {
                    double cantidadPosible = Math.Min(Bateria.BateriaActual, robot.Bateria.BateriaMax - robot.Bateria.BateriaActual);
                    ConsumirEnergia(cantidadPosible);
                    robot.RecargarBateria(cantidadPosible);
                }
            }
        }

        public void TransferirCargaFisica(Operador robot, double cantidad)
        {
            if (!Daños["ServoAtascado"])
            {
                if (ubicacion != robot.ubicacion) Mover(robot.ubicacion, false);

                if (CargaActual - cantidad >= 0 && robot.CargaActual + cantidad <= robot.CargaMax)
                {
                    CargaActual -= cantidad;
                    robot.CargaActual += cantidad;
                }

                else
                {
                    double cantidadPosible = Math.Min(CargaActual, robot.CargaMax - robot.CargaActual);
                    CargaActual -= cantidadPosible;
                    robot.CargaActual += cantidadPosible;
                }

            }
           
        }


        public double ObtenerVelocidad()
        {
            double porcentajeCarga = CargaActual / CargaMax;

            double porcentajeAReducir = porcentajeCarga * 0.05 / 0.1;

            double nuevaVelocidad = VelocidadOptima - VelocidadOptima * porcentajeAReducir;

            return nuevaVelocidad;

        }

        public double CalcularTiempoDeViaje(int distancia)
        {
            double velocidad = ObtenerVelocidad();

            double tiempo = distancia / velocidad;

            return tiempo;
        }

        public double CalcularGastoDeBateria(double tiempo)
        {
            double gastoDeBateria;

            if (!Daños["BateriaPerforada"]) gastoDeBateria = tiempo * 1000;

            else gastoDeBateria = tiempo * 5000;

            return gastoDeBateria;
        }

        public void RecargarBateria(double cantBateria)
        {
            if (!Daños["BateriaPerforada"])
            {
                Bateria.CargarBateria(cantBateria);
            }
        }

        public void ConsumirEnergia(double cantBateria)
        {
            Bateria.ConsumirBateria(cantBateria);
        }

        public void Cargar()
        {
            if (!Daños["ServoAtascado"])
            {
                CargaActual = CargaMax;
            }
                
        }

        public void Descargar()
        {
            if (!Daños["ServoAtascado"])
            {
                CargaActual = 0;
            }

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

        public bool SimularDaño()
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
                if (SimularDaño())
                {
                    if (elem.Key == "MotorComprometido")
                    {
                        ComprometerMotor();
                    }
                    else Daños[elem.Key] = true;
                }
            }
        }

        public bool ExisteDaño()
        {
            //Código

            return true;
        }

        public void Reparar()
        {
            foreach (KeyValuePair<string, bool> elem in Daños)
            {
                Daños[elem.Key] = false;
            }
        }



        public void cambiarEstado(EnumEstadoOperador nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}
