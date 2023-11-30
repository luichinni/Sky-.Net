using SkyNet.Entidades.Fabricas;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class CargarSimulacionCmd : Comando
    {
        Menu _menu;
        string carpeta = "simulaciones";
        string _ruta;
        string _rutaLog;
        string _cancelar = "Cancelar";
        public CargarSimulacionCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            _ruta = Directory.GetCurrentDirectory() + "\\" + carpeta;
            _rutaLog = Directory.GetCurrentDirectory();
            _menu = new MenuConcreto(ActualizarSimulaciones(),nombre);
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            bool carga = false;
            _menu.Opciones = ActualizarSimulaciones();
            _menu.Mostrar();
            string nombre = _menu.GetSeleccion();
            if (nombre != _cancelar) 
            {
                carga = IntentarCargarDatosDeMundo(m, _ruta + "\\"+ nombre) && IntentarCargarConfiguracion(m, _ruta + "\\" + nombre);
            }
            return carga;
        }
        private bool IntentarCargarConfiguracion(Mundo m, string nombre)
        {
            bool pudo = false;
            try
            {
                string jsonConfig = File.ReadAllText(nombre + "\\WorldConfig.json");
                int[][] config = JsonSerializer.Deserialize<int[][]>(jsonConfig);
                /*
                 int[][] configuraciones = { 
                    m.ExpansionZonal,
                    m.ExtensionZonal,
                    m.PrioridadZonal,
                    m.MaximaAparicion,
                    new int[] { m.CantCuarteles,
                                Fabrica.Id}
                };
                 */
                m.ExpansionZonal = config[0];
                m.ExtensionZonal = config[1];
                m.PrioridadZonal = config[2];
                m.MaximaAparicion = config[3];
                m.MaxCuarteles = config[4][0];
                Fabrica.Id = config[4][1];
                pudo = true;
            }catch (Exception ex)
            {
                string logPath = _rutaLog + "\\" + "log.txt";
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath,"");
                }
                string contenido = File.ReadAllText(logPath);
                contenido += "\n" +"["+ DateTime.Now + "] Cargar Config: " + ex.Message;
                File.WriteAllText(logPath, contenido);
            }
            return pudo;
        }
        private bool IntentarCargarDatosDeMundo(Mundo m, string nombre)
        {
            bool pudo = false;
            try
            {
                string jsonMundo = File.ReadAllText(nombre + "\\WorldData.json");
                Dictionary<string, Localizacion> localizaciones = JsonSerializer.Deserialize<Dictionary<string, Localizacion>>(jsonMundo);
                m.ReanudarSimulacion(localizaciones);
                pudo = true;
            }
            catch (Exception ex)
            {
                string logPath = _rutaLog + "\\" + "log.txt";
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath, "");
                }
                string contenido = File.ReadAllText(logPath);
                contenido += "\n" + "[" + DateTime.Now + "] Cargar Datos: " + ex.Message;
                File.WriteAllText(logPath, contenido);
            }
            return pudo;
        }
        private string[] ActualizarSimulaciones()
        {
            string[] carpetas = new string[0];
            try
            {
                carpetas = Directory.GetDirectories(_ruta);
                carpetas = carpetas.Select(c => Path.GetFileName(c)).Append(_cancelar).ToArray();
            }
            catch (Exception ex)
            {
                string logPath = _rutaLog +"\\"+ "log.txt";
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath, "");
                }
                string contenido = File.ReadAllText(logPath);
                contenido += "\n" + "[" + DateTime.Now + "] Actualizar Simulacion: " + ex.Message;
                File.WriteAllText(logPath, contenido);
            }
            return carpetas;
        }
    }
}
