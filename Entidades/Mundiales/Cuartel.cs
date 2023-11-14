using System;
using SkyNet.Entidades.Operadores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern.Comandos;

namespace SkyNet.Entidades.Mundiales
{
    public class Cuartel
    {
        private string id;
        private Localizacion ubicacion;
       // Primer intento, paciencia por favor hahahaha 
       // La primera premisa es Listar el Estado de todos los operadores
       // Cree el Enum de los estados, pero debería ir dentro del operador
       // o colocamos por default el estado inicial inactivo
       // private Dictionary<string, Operador> ListaDeOperadores = new Dictionary<string, Operador>();
        


        public string Identificacion()
        {
            return id;
        }

        public Localizacion GetUbicacion()
        {
            return ubicacion;
        }

       /* 
        public ListarOperadores(Dictionary<string, Operador> ListaDeOperadores)
        {
            foreach (KeyValuePair<string, Operador> operador in ListaDeOperadores)
            { 
            Console.WriteLine(ListaDeOperadores)
            }
        }*/



    }
}
