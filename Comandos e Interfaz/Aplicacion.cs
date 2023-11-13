using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public class Aplicacion
    {
        private List<string> _titulos;
        private Dictionary<string, string[]> _opciones;
        private string _salida;
        private Menu _menu;
        private Invocador _invocador;
        public Aplicacion(Menu menu, Invocador invocador ,List<string> titulos, Dictionary<string, string[]> opciones, string salida = "Salir")
        {
            /// Cada posicion de la lista de titulos es usada para buscar sus opciones correspondientes
            /// si alguna de las opciones se llama como otro titulo, se navega hasta ese menu, todas las opciones deben
            /// ser comandos validos, en caso de no serlos, esa opcion no hace nada, el string salida es el texto
            /// que aparece en todos los menus para cerrar el programa, en caso de querer navegar hacia un menu anterior,
            /// es necesario añadirlo como opcion (podria tener una lista de donde viene cada menu y hacer un "Volver"
            /// pero prefiero dejarlo asi)
            _menu = menu;
            _invocador = invocador;

            if(opciones != null) _opciones = new Dictionary<string, string[]>(opciones);
            else _opciones = new Dictionary<string, string[]>();

            if (titulos != null) _titulos = new List<string>(titulos);
            else _titulos = new List<string>();

            _salida = salida;
        }
        public void MainLoop()
        {
            Mundo mundo = Mundo.GetInstance();
            Cuartel cuartelActual = null;
            Comando cmd;
            string seleccion;
            bool fin = false;
            _menu.Titulo = _titulos[0];
            _menu.Opciones = GetOpciones(_titulos[0]);

            while (!fin)
            {
                Console.ResetColor();
                _menu.Mostrar();
                seleccion = _menu.GetSeleccion();
                if (seleccion != _salida && seleccion != "Limpiar Pantalla")
                { /// si no es salida intentamos ejecutar comando y ver si hay otro menu
                    cmd = _invocador.GetComando(seleccion);
                    if (cmd != null)
                        cmd.Ejecutar(mundo, ref cuartelActual);

                    if (_titulos.Contains(seleccion))
                    { /// si a su vez es otro menu, se cambia
                        _menu.Titulo = seleccion;
                        _menu.Opciones = GetOpciones(seleccion);
                    }
                }
                else if (seleccion == "Limpiar Pantalla") Console.Clear();
                else fin = true; /// si la salida
            }
        }
        private string[] GetOpciones(string titulo)
        {
            /// Obtenemos las opciones del titulo correspondiente
            /// si ya tiene la opcion de salida no se le agrega
            string[] op;
            _opciones.TryGetValue(titulo, out op);
            if (!op.Contains(_salida))
            {
                Array.Resize(ref op, op.Length + 1);
                op[op.Length - 1] = _salida;
            }
            Array.Resize(ref op, op.Length + 1);
            op[op.Length - 1] = "Limpiar Pantalla";
            return op;
        }
        public void AgregarMenu(string titulo, string[] opciones) 
        {
            if (opciones != null && titulo != null) 
            { 
                _titulos.Add(titulo);
                _opciones.Add(titulo, opciones);
            }
        }
    }
}
