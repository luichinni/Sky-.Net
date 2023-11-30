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
            Gps = new GPS();
            ZonasPeligrosas = new List<EnumTiposDeZona>() { EnumTiposDeZona.VertederoElectronico, EnumTiposDeZona.Vertedero };
            Daños = new Dictionary<string, bool>
            {
                { "MotorComprometido",false },
                { "ServoAtascado",false },
                { "BateriaPerforada",false },
                { "PuertoBateriaDesconectado",false },
                { "PinturaRayada",false },
                { "BateriaCargaDañada",false}
            };

        }


        //Métodos de movimiento
       

        public void Mover(Localizacion nuevaUbicacion, bool rutaDirecta)
        {
            cambiarEstado(EnumEstadoOperador.Active);

            Localizacion ubicacion = getUbicacion();

            List<EnumTiposDeZona> zonasPeligrosasDirecto = new List<EnumTiposDeZona>(ZonasPeligrosas); //Copia para no modificar la original

            zonasPeligrosasDirecto.Remove(EnumTiposDeZona.VertederoElectronico); // El camino directo no evade los vertederos

            zonasPeligrosasDirecto.Remove(EnumTiposDeZona.Vertedero);

            EnumTiposDeZona[] arrayZonasPeligrosas = ZonasPeligrosas.ToArray(); //Se pasa a array para poder usarlo en GetCamino

            List<Localizacion> camino = Gps.GetCamino(ubicacion, nuevaUbicacion, arrayZonasPeligrosas);

            if (camino == null || rutaDirecta)  //Si camino es null no se pudo encontrar una ruta óptima, entonces toma la ruta directa
            {
                arrayZonasPeligrosas = zonasPeligrosasDirecto.ToArray();
                camino = Gps.GetCamino(ubicacion, nuevaUbicacion, arrayZonasPeligrosas);
            }

            for (int i = 1; i < camino.Count; i++)
            {
                int distancia = Gps.CalcularDistancia(ubicacion, camino[i]);

                double tiempo = CalcularTiempoDeViaje(distancia);

                if (CalcularGastoDeBateria(tiempo) <= Bateria.ConsultarBateria())
                {
                    ubicacion.Salir(Id);

                    ubicacion = camino[i];

                    ubicacion.Entrar(Id);

                    Bateria.ConsumirBateria(CalcularGastoDeBateria(tiempo));

                    if (ubicacion.TipoZona == EnumTiposDeZona.Vertedero) Dañar();

                    else if (ubicacion.TipoZona == EnumTiposDeZona.VertederoElectronico) DañarBateria();

                    CoordX = ubicacion.coordX;
                    CoordY = ubicacion.coordY;
                }
            }

            cambiarEstado(EnumEstadoOperador.Inactive);
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

        public void Reciclar()
        {
            if (!Daños["ServoAtascado"])
            {
                cambiarEstado(EnumEstadoOperador.Active);

                Localizacion ubicacion = getUbicacion();

                Localizacion destino = Gps.BuscarCercano(EnumTiposDeZona.Vertedero, ubicacion);

                Mover(destino, true);

                Cargar();

                destino = Gps.BuscarCercano(EnumTiposDeZona.SitioReciclaje, ubicacion);

                Mover(destino, true);

                Descargar();

                cambiarEstado(EnumEstadoOperador.Inactive);
            }

            
        }

        
        //Métodos de operaciones con la batería

        public void TransferirBateria(Operador robot, double cantidad)
        {
            if (!Daños["PuertoBateriaDesconectado"] && !robot.Daños["PuertoBateriaDesconectado"])
            {
                cambiarEstado(EnumEstadoOperador.Active);
                robot.cambiarEstado(EnumEstadoOperador.Active);

                if (getUbicacion() != robot.getUbicacion()) Mover(robot.getUbicacion(), false);

                if (Bateria.BateriaActual - cantidad >= 0 && robot.Bateria.BateriaActual + cantidad <= robot.Bateria.BateriaMax)
                {
                    Bateria.BateriaActual -= cantidad;
                    robot.Bateria.BateriaActual += cantidad;
                }
                else
                {
                    double cantidadPosible = Math.Min(Bateria.BateriaActual, robot.Bateria.BateriaMax - robot.Bateria.BateriaActual);
                    Bateria.BateriaActual -= cantidadPosible;
                    robot.Bateria.BateriaActual += cantidadPosible;
                }

                cambiarEstado(EnumEstadoOperador.Inactive);
                robot.cambiarEstado(EnumEstadoOperador.Inactive);
            }


        }

        public void RecargarBateria(double cantBateria)
        {
            if (!Daños["PuertoBateriaDesconectado"] && (getUbicacion().TipoZona == EnumTiposDeZona.Cuartel || getUbicacion().TipoZona == EnumTiposDeZona.SitioReciclaje))
            {
                Bateria.CargarBateria(cantBateria);
            }
        }

        public void ConsumirEnergia(double cantBateria)
        {
            Bateria.ConsumirBateria(cantBateria);
        }

        public double GetBateria()
        {
            return Bateria.ConsultarBateria();
        }


        //Métodos de Carga Física


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

        public void TransferirCargaFisica(Operador robot, double cantidad)
        {

            if (!Daños["ServoAtascado"] && !robot.Daños["ServoAtascado"])
            {
                cambiarEstado(EnumEstadoOperador.Active);
                robot.cambiarEstado(EnumEstadoOperador.Active);

                if (getUbicacion() != robot.getUbicacion()) Mover(robot.getUbicacion(), false);

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

                cambiarEstado(EnumEstadoOperador.Inactive);
                robot.cambiarEstado(EnumEstadoOperador.Inactive);
            }

        }


        //Métodos de Daños

        public bool SimularDaño()
        {
            int daño;

            Random randy = new Random();

            daño = randy.Next(1, 101);

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


        public void DañarBateria()
        {
            if (Daños["BateriaCargaDañada"] == false)
            {
                Bateria.BateriaMax -= Bateria.BateriaMax * 20 / 100;
                Bateria.BateriaActual -= Bateria.BateriaActual * 20 / 100;  //Reduzco para prevenir que no quede con más valor que la máxima
                Daños["BateriaCargaDañada"] = true;
            }
        }

        public void Dañar()
        {
            foreach (KeyValuePair<string, bool> elem in Daños)
            {
                if (SimularDaño() && elem.Key != "BateriaCargadaDañada")
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
            bool existe = Daños.Values.Any(valor => valor);

            return existe;
        }

        public void CambiarBateria()
        {
            double reestablecerBateriaMaxima = Bateria.BateriaMax;
            reestablecerBateriaMaxima += (reestablecerBateriaMaxima * 20 / 80);
            Bateria = new Bateria();
            Bateria.BateriaMax = reestablecerBateriaMaxima;
            Bateria.BateriaActual = reestablecerBateriaMaxima;
        }

        public void Reparar()
        {
            foreach (KeyValuePair<string, bool> elem in Daños)
            {
                if (elem.Key == "MotorComprometido" && elem.Value)
                {
                    VelocidadOptima *= 2;
                }

                else if (elem.Key == "BateriaCargaDañada" && elem.Value)
                {
                    CambiarBateria();
                }

                Daños[elem.Key] = false;
            }
        }


        // Otros Métodos

        public void cambiarEstado(EnumEstadoOperador nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public string GetId() { return Id; }


        public Localizacion getUbicacion()
        {
            Localizacion ubicacion = Mundo.GetInstance().GetLocalizacion(CoordX, CoordY);
            return ubicacion;

        }

    }
}
