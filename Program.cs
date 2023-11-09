// See https://aka.ms/new-console-template for more information
using SkyNet.Entidades.Operadores;

Console.WriteLine("Hello, World!");




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

