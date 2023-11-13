using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    class ConfigurarSimulacionCmd : Comando
    {
        public ConfigurarSimulacionCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            /// Se podria haber metido subcomandos o algo asi pero era demasiado
            Console.ResetColor();
            string[] opciones = new string[] { "Tamaño horizontal", "Tamaño vertical", 
                                                "Cantidad sitios de reciclaje", 
                                                "Proporcion generacion del agua",
                                                "Proporcion generacion vertederos generales",
                                                "Proporcion generacion vertederos normales",
                                                "Proporcion generacion vertederos electronicos",
                                                "Proporcion generacion planicies",
                                                "Proporcion generacion bosques",
                                                "Proporcion generacion terrenos baldios",
                                                "Proporcion generacion urbanismo",
                                                "Salir"
            };
            Menu menu = new MenuConcreto(opciones,"¿Que paramtro desea configurar?");
            bool fin = false;
            while (!fin)
            {
                Console.Clear();
                menu.Mostrar();
                string seleccion = menu.GetSeleccion();
                switch (seleccion)
                {
                    case "Tamaño horizontal": ModificarX(); break;
                    case "Tamaño vertical": ModificarY(); break;
                    case "Cantidad sitios de reciclaje": ModificarRecicladores(); break;
                    case "Proporcion generacion del agua": ModificarAgua(); break;
                    case "Proporcion generacion vertederos generales": ModificarVertederos(); break;
                    case "Proporcion generacion vertederos normales": ModificarVerteNormal(); break;
                    case "Proporcion generacion vertederos electronicos": ModificarVerteElect(); break;
                    case "Proporcion generacion planicies": ModificarPlanicie(); break;
                    case "Proporcion generacion bosques": ModificarBosque(); break;
                    case "Proporcion generacion terrenos baldios": ModificarBaldios(); break;
                    case "Proporcion generacion urbanismo": ModificarUrbanismo(); break;
                    case "Salir": fin = true; break;
                }
            }
        }
        private void ModificarUrbanismo()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de urbanismo:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeUrbanismo = int.Parse(tamaño);
        }
        private void ModificarBaldios()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de baldios:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeBaldios = int.Parse(tamaño);
        }
        private void ModificarBosque()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de bosque:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeBosques = int.Parse(tamaño);
        }
        private void ModificarPlanicie()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de planicie:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajePlanicies = int.Parse(tamaño);
        }
        private void ModificarVerteElect()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de vertederos electronicos:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeVertederoElectronico = int.Parse(tamaño);
        }
        private void ModificarVerteNormal()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de vertederos normales:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeVertederoNormal = int.Parse(tamaño);
        }
        private void ModificarVertederos()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion de vertederos generales (normales y electronicos):");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeDeVertederos = int.Parse(tamaño);
        }
        private void ModificarAgua()
        {
            Console.Clear();
            Console.WriteLine("Nueva proporcion de generacion del agua:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.PorcentajeDeAgua = int.Parse(tamaño);
        }
        private void ModificarRecicladores()
        {
            Console.Clear();
            Console.WriteLine("Nueva cantidad de puntos de reciclado:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.MaxSitiosReciclaje = int.Parse(tamaño);
        }
        private void ModificarY()
        {
            Console.Clear();
            Console.WriteLine("Nuevo tamaño vertical:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.MaxCoordY = int.Parse(tamaño);
        }
        private void ModificarX()
        {
            Console.Clear();
            Console.WriteLine("Nuevo tamaño horizontal:");
            string tamaño = Console.ReadLine();
            while (!EsNumerico(tamaño)) tamaño = Console.ReadLine();
            Mundo.MaxCoordX = int.Parse(tamaño);
        }
        private bool EsNumerico(string str)
        {
            int cantNumeros = 0;
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9') cantNumeros++;
            }
            return cantNumeros == str.Length && str != "";
        }
    }
}
