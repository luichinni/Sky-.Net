using SkyNet.CommandPattern;
using SkyNet.CommandPattern.Comandos;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System.Reflection.Emit;

/// Inicializacion de cosas
Random rand = new Random();
bool fin = false;
string seleccion;
Mundo mundito = Mundo.GetInstance();
Cuartel cuartelito = null;

/// SetComandos Mundo
Invocador invocador = new Invocador();
invocador.AgregarComando(new ImprimirSectorCmd("Cargar Sector", "Carga el fragmento de sector en la interfaz"));

/// SetComandos Menu
Invocador invocadorMenu = new Invocador();
invocadorMenu.AgregarComando(new CrearSimulacionCmd("Crear Simulacion", "Crea una nueva simulacion a partir de los parametros configurados"));
invocadorMenu.AgregarComando(new CargarSimulacionCmd("Cargar Simulacion", "Carga una simuacion antigua desde memoria"));
invocadorMenu.AgregarComando(new ConfigurarSimulacionCmd("Configurar Nueva Simulacion", "Configura los parametros para una nueva simulacion"));

/// Menu
List<string> strings = new List<string>(invocadorMenu.GetNombreComandos());
strings.Add("Salir");
string titulo = "Sky .Net Menu Simulaciones";
Menu menu = new MenuConcreto(strings.ToArray(), titulo.PadLeft(titulo.Length+8,'-').PadRight(titulo.Length + 16, '-'));

/// Fin inicializacion

while (!fin)
{
    Console.ResetColor();
    Console.Clear();
    menu.Mostrar();
    seleccion = menu.GetSeleccion();
    if (seleccion != "Salir") invocadorMenu.GetComando(seleccion).Ejecutar(mundito,cuartelito);
    else fin = true;
}