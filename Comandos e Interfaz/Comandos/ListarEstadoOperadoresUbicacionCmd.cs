using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;

namespace SkyNet.CommandPattern.Comandos
{
    public class ListarEstadoOperadoresUbicacionCmd : Comando
    {
        Menu _menu;
        string _cancelar = "Cancelar";
        public ListarEstadoOperadoresUbicacionCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            string[] opciones = new string[] { _cancelar };
            _menu = new MenuConcreto(opciones,nombre);
        }
        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            int[] coord = GetCoordenadas(Mundo.GetInstance().MaxCoordX, Mundo.GetInstance().MaxCoordY);
            Localizacion l = m.GetLocalizacion(coord[0], coord[1]);
            Cuartel cuartelAux = c;
            string[] idsOps = l.GetOperadores().ToArray();
            idsOps = idsOps.Select(id => id + ":" + cuartelAux.Operadores.Find(op => op.Id == id).Estado).ToArray();

            for (int i = 0; i < idsOps.Length; i++)
            {
                Dictionary<string, bool> fallas = c.Operadores.Find(oper => oper.Id == idsOps[i].Split(':')[0]).Daños;
                string strEstado = idsOps[i] + "; Daños[";
                foreach (KeyValuePair<string, bool> falla in fallas)
                {
                    if (falla.Value) strEstado += falla.Key + ", ";
                }
                if (strEstado.Length > (idsOps[i] + "; Daños[").Length) strEstado.Remove(strEstado.Length - 1);
                strEstado += "]";
                idsOps[i] = strEstado;
            }

            _menu.Opciones = idsOps;
            _menu.Mostrar();
            return true;
        }
        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            ConsoleHelper.EscribirCentrado("Escriba la coordenada X,Y de la ubicacion que quiera listar el estado de operadores");
            ConsoleHelper.EscribirCentrado($"Tamaño de la simulacion actual: 0,0 -> {Mundo.GetInstance().MaxCoordX - 1},{Mundo.GetInstance().MaxCoordY - 1}");
            Console.CursorLeft = Console.WindowWidth / 2 - 4;

            string coords = Console.ReadLine();
            string[] coordsSplit = coords.Replace(" ", "").Split(','); // quita blancos innecesarios y separa por comas
                                                                       // hay que ingresar 2 coordenadas

            while (coordsSplit.Length < 2 || !SonNumeros(coordsSplit) || !ComprobarMaximos(coordsSplit))
            {
                coords = Console.ReadLine();
                coordsSplit = coords.Replace(" ", "").Split(',');
            }

            int[] intCoords = new int[coordsSplit.Length];
            for (int i = 0; i < intCoords.Length; i++)
            {
                intCoords[i] = int.Parse(coordsSplit[i]);
            }
            return intCoords;
        }
        private bool ComprobarMaximos(string[] str)
        {
            bool valido = false;
            if (int.Parse(str[0]) < Mundo.GetInstance().MaxCoordX && int.Parse(str[1]) < Mundo.GetInstance().MaxCoordY)
                valido = true;
            return valido;
        }
        private bool SonNumeros(string[] str)
        {
            bool son = true;
            foreach (string str2 in str)
            {
                int cantNumeros = 0;
                foreach (char c in str2)
                {
                    if (c >= '0' && c <= '9') cantNumeros++;
                }
                son = son && (cantNumeros == str2.Length && str2 != "");
            }
            return son;
        }
    }
}
