using SkyNet.Entidades.Grafo;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class GuardarSimulacionCmd : Comando
    {
        string path = Directory.GetCurrentDirectory();
        string carpeta = "simulaciones";
        public GuardarSimulacionCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            ConsoleHelper.WriteTitulo("Guardar Simulacion", ConsoleColor.Green);
            string archivo = GetNombreArchivo();
            string camino = path + "\\" + carpeta;
            GuardarMapa(camino+"\\"+archivo);
        }
        private void GuardarMapa(string camino)
        {
            Dictionary<string, Localizacion> dicGuardar = new Dictionary<string, Localizacion>();

            foreach (KeyValuePair<string, IVertice<Localizacion>> vertice in Mundo.GetInstance().Mapamundi)
            {
                dicGuardar.Add(vertice.Key, vertice.Value.GetDato());
            }
            File.WriteAllText(camino + ".json", JsonSerializer.Serialize(dicGuardar));
        }
        private string GetNombreArchivo()
        {
            ConsoleHelper.EscribirCentrado("Ingrese el nombre de la simulacion:");
            Console.CursorLeft = Console.WindowWidth / 2 - 10;
            string nombre = Console.ReadLine().ToUpper();
            string aux = (nombre == "OVERRIDE") ? "SimulacionSinNombre" + new Random().Next(1000) : nombre;
            while (File.Exists(path + "\\"+carpeta+"\\" + nombre) && nombre != "OVERRIDE")
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
