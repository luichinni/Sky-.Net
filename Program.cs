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
invocador.AgregarComando(new AgregarReservaCmd("Agregar Operador a Reserva", "Se lleva un operador a cuartel y se pone en reserva"));
invocador.AgregarComando(new CargarSimulacionCmd("Cargar Simulacion", "Carga una simuacion antigua desde memoria"));
invocador.AgregarComando(new ConfigurarSimulacionCmd("Configurar Nueva Simulacion", "Configura los parametros para una nueva simulacion"));
invocador.AgregarComando(new CrearCuartelCmd("Crear Cuartel", "Se crea un nuevo cuartel en el mundo sin superar el maximo configurado"));
invocador.AgregarComando(new CrearSimulacionCmd("Crear Simulacion", "Crea una nueva simulacion a partir de los parametros configurados"));
invocador.AgregarComando(new DescargarEnCuartelCmd("Descargar Operador en Cuartel", "Se descarga la carga fisica del operador en su cuartel"));
invocador.AgregarComando(new GuardarSimulacionCmd("Guardar Simulacion", "Se almacena en memoria el estado actual de la simulacion"));
invocador.AgregarComando(new ImprimirSectorCmd("Cargar Mapa de Sector", "Carga el fragmento de sector en la interfaz"));
invocador.AgregarComando(new ListarEstadoOperadoresCmd("Listar Estado de Operadores", "Se lista el estado fisico de los operadores"));
invocador.AgregarComando(new ListarEstadoOperadoresUbicacionCmd("Listar Estado de Operadores En Ubicacion", "Se lista el estado de operadores en cierta ubicacion"));
invocador.AgregarComando(new MoverOperadorCmd("Mover Operador a Nueva Ubicacion","Se intenta posicionar un operador en una nueva localizacion"));
invocador.AgregarComando(new PonerStandbyCmd("Poner Operador en Standby","Se desactiva el operador seleccionado"));
invocador.AgregarComando(new RecallOperadorUnicoCmd("Recall de un Operador","Se llama a un operador a cuartel"));
invocador.AgregarComando(new RecargarBateriaCuartelCmd("Recargar Bateria de Operador", "Se llama al operador a cuartel para cargar su bateria"));
invocador.AgregarComando(new RemoverReservaCmd("Quitar Operador de Reserva", "Saca del cuartel el operador indicado"));
invocador.AgregarComando(new TotalRecallCmd("Total Recall","Se notifica a todos los operadores del cuartel que regresen"));
invocador.AgregarComando(new TransferirBateriaCmd("Transferir Bateria entre Operadores","Se transfiere bateria del operador A al B si se encuentran en la misma ubicacion"));
invocador.AgregarComando(new TransferirCargaFisicaCmd("Transferir Carga entre Operadores","Se transfiere carga fisica del operador A al B si se encuentran en la misma ubicacion"));

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
    { "Iniciar Simulacion" , new string[] {
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