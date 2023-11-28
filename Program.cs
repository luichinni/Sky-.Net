using SkyNet;
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

mundito.ExpansionZonal[(int)EnumTiposDeZona.Lago] = 10;
mundito.ExpansionZonal[(int)EnumTiposDeZona.Planicie] = 0; // no hace falta porq el mundo inicia todo planicie
mundito.ExpansionZonal[(int)EnumTiposDeZona.Bosque] = 12;
mundito.ExpansionZonal[(int)EnumTiposDeZona.VertederoElectronico] = 2;
mundito.ExpansionZonal[(int)EnumTiposDeZona.Vertedero] = 2;
mundito.ExpansionZonal[(int)EnumTiposDeZona.SectorUrbano] = 3;
mundito.ExpansionZonal[(int)EnumTiposDeZona.TerrenoBaldio] = 1;
mundito.ExpansionZonal[(int)EnumTiposDeZona.SitioReciclaje] = 1; // no debe expandirse?
mundito.ExpansionZonal[(int)EnumTiposDeZona.Cuartel] = 0; // no debe haber

mundito.ExtensionZonal[(int)EnumTiposDeZona.Lago] = 10;
mundito.ExtensionZonal[(int)EnumTiposDeZona.Planicie] = 0; // no hace falta porq el mundo inicia todo planicie
mundito.ExtensionZonal[(int)EnumTiposDeZona.Bosque] = 10;
mundito.ExtensionZonal[(int)EnumTiposDeZona.VertederoElectronico] = 3;
mundito.ExtensionZonal[(int)EnumTiposDeZona.Vertedero] = 2;
mundito.ExtensionZonal[(int)EnumTiposDeZona.SectorUrbano] = 10;
mundito.ExtensionZonal[(int)EnumTiposDeZona.TerrenoBaldio] = 5;
mundito.ExtensionZonal[(int)EnumTiposDeZona.SitioReciclaje] = 1; // no debe extenderse?
mundito.ExtensionZonal[(int)EnumTiposDeZona.Cuartel] = 0; // no debe haber

mundito.MaximaAparicion[(int)EnumTiposDeZona.Lago] = 25;
mundito.MaximaAparicion[(int)EnumTiposDeZona.Planicie] = 0; // no hace falta porq el mundo inicia todo planicie
mundito.MaximaAparicion[(int)EnumTiposDeZona.Bosque] = 25;
mundito.MaximaAparicion[(int)EnumTiposDeZona.VertederoElectronico] = 12;
mundito.MaximaAparicion[(int)EnumTiposDeZona.Vertedero] = 12;
mundito.MaximaAparicion[(int)EnumTiposDeZona.SectorUrbano] = 12;
mundito.MaximaAparicion[(int)EnumTiposDeZona.TerrenoBaldio] = 8;
mundito.MaximaAparicion[(int)EnumTiposDeZona.SitioReciclaje] = 5; // no debe extenderse?
mundito.MaximaAparicion[(int)EnumTiposDeZona.Cuartel] = 0; // no debe haber

mundito.PrioridadZonal[0] = (int)EnumTiposDeZona.Planicie;
mundito.PrioridadZonal[1] = (int)EnumTiposDeZona.Bosque;
mundito.PrioridadZonal[2] = (int)EnumTiposDeZona.TerrenoBaldio;
mundito.PrioridadZonal[3] = (int)EnumTiposDeZona.SectorUrbano;
mundito.PrioridadZonal[4] = (int)EnumTiposDeZona.Vertedero;
mundito.PrioridadZonal[5] = (int)EnumTiposDeZona.VertederoElectronico;
mundito.PrioridadZonal[6] = (int)EnumTiposDeZona.SitioReciclaje;
mundito.PrioridadZonal[7] = (int)EnumTiposDeZona.Cuartel;
mundito.PrioridadZonal[8] = (int)EnumTiposDeZona.Lago;

/// SetComandos -> PODRIA HACERSE DIFERENTE pero ya ta
Invocador invocador = new Invocador();
invocador.AgregarComando(new AgregarReservaCmd("Agregar Operador a Reserva", "Se lleva un operador a cuartel y se pone en reserva"));
invocador.AgregarComando(new CargarSimulacionCmd("Cargar Simulacion", "Carga una simuacion antigua desde memoria"));
invocador.AgregarComando(new ConfigExtensionZonalCmd("Configurar Extensiones Zonales", "Apartado para configurar las extensiones de cada zona"));
invocador.AgregarComando(new ConfigExpansionZonalCmd("Configurar Expansiones Zonales", "Apartado para configurar las extensiones de cada zona"));
invocador.AgregarComando(new ConfigPrioridadZonalCmd("Configurar Prioridades Zonales", "Apartado para configurar las prioridades de cada zona, es decir, en que orden se generan"));
invocador.AgregarComando(new ConfigMaxAparicionZonalCmd("Configurar Cantidad Maxima de Aparcion Zonal", "Apartado para configurar la cantidad de iteraciones de cada zona"));
invocador.AgregarComando(new CrearCuartelCmd("Crear Cuartel", "Se crea un nuevo cuartel en el mundo sin superar el maximo configurado"));
invocador.AgregarComando(new CrearSimulacionCmd("Crear Simulacion", "Crea una nueva simulacion a partir de los parametros configurados"));
invocador.AgregarComando(new DescargarEnCuartelCmd("Descargar Operador en Cuartel", "Se descarga la carga fisica del operador en su cuartel"));
invocador.AgregarComando(new GuardarSimulacionCmd("Guardar Simulacion", "Se almacena en memoria el estado actual de la simulacion"));
invocador.AgregarComando(new ImprimirSectorCmd("Cargar Mapa de Sector", "Carga el fragmento de sector en la interfaz"));
invocador.AgregarComando(new LimpiarPantallaCmd("Limpiar Pantalla", "Limpia la pantalla xd"));
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
    { "Configurar Nueva Simulacion", new string[] {
        "Configurar Extensiones Zonales",
        "Configurar Expansiones Zonales",
        "Configurar Cantidad Maxima de Aparcion Zonal",
        "Configurar Prioridades Zonales",
        "Sky .Net Menu Simulaciones"
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
    { "Cambiar Cuartel" , new string[] {
        "Crear Cuartel",
        "Cuartel"
    }},
    { "Cuartel" , new string[] {
        "Cargar Mapa de Sector",
        "Agregar Operador a Reserva",
        "Quitar Operador de Reserva",
        "Descargar Operador en Cuartel",
        "Recargar Bateria de Operador",
        "Listar Estado de Operadores",
        "Listar Estado de Operadores En Ubicacion",
        "Mover Operador a Nueva Ubicacion",
        "Poner Operador en Standby",
        "Recall de un Operador",
        "Total Recall",
        "Transferir Bateria entre Operadores",
        "Transferir Carga entre Operadores",
        "Guardar Simulacion",
        "Cambiar Cuartel",
        "Sky .Net Menu Simulaciones"
    }},
};
List<string> titulos = opciones.Keys.ToList();
Menu menu = new MenuConcreto(null,null,'-');

Aplicacion app = new Aplicacion(menu,invocador,titulos,opciones,"Cerrar Programa");
/// Fin inicializacion

ConsoleHelper.AdvertenciaTamaño();

app.MainLoop();