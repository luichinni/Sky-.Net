// See https://aka.ms/new-console-template for more information
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

int origRow = Console.CursorTop;
int origCol = Console.CursorLeft;
Random rand = new Random();
Mundo mundito = Mundo.GetInstance();
ConsoleColor c;

for (int i=0; i<Mundo.MaxCoordX*3; i+=3)
{
    for(int j=0; j<Mundo.MaxCoordY*2; j+=2)
    {
        c = (ConsoleColor)Enum.GetValues(typeof(EnumColoresZona)).GetValue((int)mundito.GetLocalizacion(i/3,j/2).GetTipoZona());
        Console.BackgroundColor = c;
        Console.ForegroundColor = ConsoleColor.White;
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i, j);
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i+1, j);
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i+2, j);
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i, j+1);
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i + 1, j+1);
        WriteAt(mundito.GetLocalizacion(i / 3, j / 2).GetTipoZona().ToString()[0].ToString(), i + 2, j+1);
        //Console.WriteLine($"POS ({i},{j}) => {mundito.GetLocalizacion(i, j).GetTipoZona()}");
    }
}

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
