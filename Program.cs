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

/// SetComandos
Invocador invocador = new Invocador();
invocador.AgregarComando(new ImprimirSectorCmd("Cargar Mapa de Sector", "Carga el fragmento de sector en la interfaz"));
invocador.AgregarComando(new CrearSimulacionCmd("Crear Simulacion", "Crea una nueva simulacion a partir de los parametros configurados"));
invocador.AgregarComando(new CargarSimulacionCmd("Cargar Simulacion", "Carga una simuacion antigua desde memoria"));
invocador.AgregarComando(new ConfigurarSimulacionCmd("Configurar Nueva Simulacion", "Configura los parametros para una nueva simulacion"));

/// Menu
Dictionary<string, string[]> opciones = new Dictionary<string, string[]>() {
    { "Sky .Net Menu Simulaciones", new string[] {
        "Crear Simulacion",
        "Cargar Simulacion",
        "Configurar Nueva Simulacion"
    }},
    { "Crear Simulacion", new string[] {
        "Crear Cuartel",
        "Cuartel"
    }},
    { "Cargar Simulacion", new string[] {
        "Iniciar Simulacion"
    }},
    { "Inciar Simulacion" , new string[] {
        "Crear Cuartel",
        "Cuartel"
    }},
    { "Cuartel" , new string[] {
        "Agregar Operador a Reserva",
        "Quitar Operador de Reserva",
        "Descargar Operador en Cuartel",
        "Recargar Bateria de Operador",
        "Cargar Mapa de Sector",
        "Listar Estado de Operadores",
        "Listar Estado de Operadores En Ubicacion",
        "Mover Operador a Nueva Ubicacion",
        "Poner Operador en Standby",
        "Recall de un Operador",
        "Total Recall",
        "Transferir Bateria entre Operadores",
        "Transferir Carga entre Operadores",
        "Guardar Simulacion",
        "Sky .Net Menu Simulaciones"
    }},
};
List<string> titulos = opciones.Keys.ToList();
Menu menu = new MenuConcreto(null,null,'-');

Aplicacion app = new Aplicacion(menu,invocador,titulos,opciones);
/// Fin inicializacion

app.MainLoop();