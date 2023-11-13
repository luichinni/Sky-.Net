// See https://aka.ms/new-console-template for more information
using SkyNet.CommandPattern;
using SkyNet.CommandPattern.Comandos;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

int origRow = Console.CursorTop;
int origCol = Console.CursorLeft;
Random rand = new Random();
Mundo.MaxCoordX = 40;
Mundo.MaxCoordY = 25;
Mundo mundito = Mundo.GetInstance();
ConsoleColor c;
int nroCuadrante = 1;
Console.WriteLine($"\t\tSeccion {nroCuadrante}");
for (int i=0; i<Mundo.MaxCoordX*2; i+=2)
{
    for(int j=0; j<Mundo.MaxCoordY; j++)
    {
        c = GetConsoleColor(mundito.GetLocalizacion(i/2,j).GetTipoZona().ToString());
        Console.BackgroundColor = c;
        Console.ForegroundColor = c;
        WriteAt("@", i+1, j+1);
        WriteAt("@", i+2, j + 1);

        //Console.WriteLine($"POS ({i},{j}) => {mundito.GetLocalizacion(i, j).GetTipoZona()}");
    }
}

foreach (EnumTiposDeZona zona in Enum.GetValues(typeof(EnumTiposDeZona)))
{
    Console.ForegroundColor = GetConsoleColor(zona.ToString());
    Console.BackgroundColor = Console.ForegroundColor;
    WriteAt("@", (Mundo.MaxCoordX*2)+2, ((int)zona) + 1);
    Console.ResetColor();
    WriteAt(zona.ToString(), (Mundo.MaxCoordX * 2) + 3, ((int)zona) + 1);
}
WriteAt("", 0, Mundo.MaxCoordY+2);

Console.ReadKey();

void WriteAt(string s, int x, int y)
{
    
    try
    {
        Console.SetCursorPosition(origCol + x, origRow + y);
        Console.Write(s);
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.Clear();
        Console.WriteLine(e.Message);
    }
}

ConsoleColor GetConsoleColor(string zona)
{
    List<ConsoleColor> colors = new List<ConsoleColor>();
    List<string> nombres = new List<string>();
    Array arr = Enum.GetValues(typeof(EnumColoresZona));
    Array arrNombre = Enum.GetNames(typeof(EnumColoresZona));
    foreach (ConsoleColor cc in arr)
    {
        colors.Add(cc);
    }
    foreach (string nombre in arrNombre)
    {
        nombres.Add(nombre);
    }
    return colors[nombres.IndexOf(zona)];
}
/*
static string GenerarID()
{
    string alfanumerico = "ABCDEFGHIJKLMNÑOPQRSTUV1234567890";

    string id = "";

    Random randy = new Random();

    for (int i = 0; i < 6; i++)
    {

        id += alfanumerico[randy.Next(1, 33)]; ;

    }

    return id;
}
*/
