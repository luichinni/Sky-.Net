// See https://aka.ms/new-console-template for more information
using SkyNet.CommandPattern;
using SkyNet.CommandPattern.Comandos;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

/// Inicializacion de cosas
Random rand = new Random();
Mundo.MaxCoordX = 40;
Mundo.MaxCoordY = 25;
Mundo mundito = Mundo.GetInstance();
Invocador invocador = new Invocador();
invocador.AgregarComando(new ImprimirSectorCmd("ImprimirSector", "Imprime el sector"));
/// Fin inicializacion

invocador.GetComando("ImprimirSector").Ejecutar(mundito, null);

Console.ReadKey();
