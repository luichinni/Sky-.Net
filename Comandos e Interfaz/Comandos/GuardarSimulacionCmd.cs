using SkyNet.Entidades.Fabricas;
using SkyNet.Entidades.Grafo;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class GuardarSimulacionCmd : Comando
    {
        string _path = Directory.GetCurrentDirectory();
        string _carpeta = "simulaciones";
        public GuardarSimulacionCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            ConsoleHelper.WriteTitulo("Guardar Simulacion", ConsoleColor.Green);
            string nombreSimulacion = GetNombreArchivo();
            string camino = _path + "\\" + _carpeta + "\\" + nombreSimulacion;
            GuardarMapa(camino);
            GuardarConfiguracionMundial(m,camino);

            return true;
        }
        private void GuardarConfiguracionMundial(Mundo m, string camino)
        {
            int[][] configuraciones = { 
                m.ExpansionZonal,
                m.ExtensionZonal,
                m.PrioridadZonal,
                m.MaximaAparicion,
                new int[] { m.CantCuarteles,
                            Fabrica.Id}
            };
            try
            {
                if (!Directory.Exists(camino)) Directory.CreateDirectory(camino);
                File.WriteAllText(camino + "\\WorldConfig.json", JsonSerializer.Serialize(configuraciones));
            }
            catch (Exception ex)
            {
                string logPath = _path + "\\log.txt";
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath, "");
                }
                string contenido = File.ReadAllText(logPath);
                contenido += "\n" + "[" + DateTime.Now + "] Guardar Config: " + ex.Message;
                File.WriteAllText(logPath, contenido);
            }
        }
        private void GuardarMapa(string camino)
        {
            Dictionary<string, Localizacion> dicGuardar = new Dictionary<string, Localizacion>();

            foreach (KeyValuePair<string, IVertice<Localizacion>> vertice in Mundo.GetInstance().Mapamundi)
            {
                dicGuardar.Add(vertice.Key, vertice.Value.GetDato());
            }

            try
            {
                if (!Directory.Exists(camino)) Directory.CreateDirectory(camino);
                File.WriteAllText(camino + "\\WorldData.json", JsonSerializer.Serialize(dicGuardar));
            }
            catch (Exception ex)
            {
                string logPath = _path + "\\log.txt";
                if (!File.Exists(logPath))
                {
                    File.WriteAllText(logPath, "");
                }
                string contenido = File.ReadAllText(logPath);
                contenido += "\n" + "[" + DateTime.Now + "] Guardar Mapa: " + ex.Message;
                File.WriteAllText(logPath, contenido);
            }
        }
        private string GetNombreArchivo()
        {
            ConsoleHelper.EscribirCentrado("Ingrese el nombre de la simulacion:");
            Console.CursorLeft = Console.WindowWidth / 2 - 10;
            string nombre = Console.ReadLine().ToUpper();
            string aux = (nombre == "OVERRIDE") ? "SimulacionSinNombre" + new Random().Next(1000) : nombre;
            while (Directory.Exists(_path + "\\"+_carpeta+"\\" + nombre) && nombre != "OVERRIDE")
            {
                ConsoleHelper.EscribirCentrado("Ya existe esa simulacion, ingrese otro nombre o la palabra 'OVERRIDE' para sobreescribirla");
                Console.CursorLeft = Console.WindowWidth / 2 - 10;
                aux = nombre;
                nombre = Console.ReadLine().ToUpper();
            }
            if (nombre != "OVERRIDE") aux = nombre;
            return aux;
        }
    }
}
