using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

namespace SkyNet.CommandPattern.Comandos
{
    public class MoverOperadorCmd : Comando
    {
        Menu _menu;
        string _cancelar = "Cancelar";
        public MoverOperadorCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            string[] operadores = new string[] { _cancelar };
            _menu = new MenuConcreto(operadores, nombre);
        }
        string[] ActualizarOperadores(Cuartel c)
        {
            return (c.Operadores.Count > 0) ? c.Operadores.Select(op => op.Id.ToString()).Append(_cancelar).ToArray() : _menu.Opciones;
        }
        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            _menu.Opciones = ActualizarOperadores(c);
            _menu.Mostrar();
            bool exito = false;
            string idSelect = _menu.GetSeleccion();
            if (idSelect != _cancelar)
            {
                Operador o = c.Operadores.Find(op => op.Id == idSelect);

                if (o.Estado != EnumEstadoOperador.StandBy)
                {
                    int[] coord = GetCoordenadas(Mundo.GetInstance().MaxCoordX, Mundo.GetInstance().MaxCoordY);
                    Localizacion l = m.GetLocalizacion(coord[0], coord[1]);

                    c.MoverOperador(o,l);
                    
                    ConsoleHelper.EscribirCentrado($"Operador {o.Id} movido hasta x{o.CoordX} y{o.CoordY}");
                    exito = true;
                }
            }
            return exito;
        }

        private int[] GetCoordenadas(int maxX, int maxY)
        {
            /// Conseguimos las coordenadas de la vista
            ConsoleHelper.EscribirCentrado("Escriba la coordenada X,Y donde quiere mover al operador");
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
