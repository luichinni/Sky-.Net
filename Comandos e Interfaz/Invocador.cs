using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern
{
    public class Invocador
    {
        Dictionary<string,Comando> comandos; // uso diccionario para mejorar busqueda
        public Invocador(Dictionary<string,Comando> comandos = null)
        {
            if (comandos != null) this.comandos = new Dictionary<string, Comando>(comandos);
            else this.comandos = new Dictionary<string, Comando>();
        }
        public Comando GetComando(string nombreCmd) // obtener comando
        {
            Comando cmdRet;
            comandos.TryGetValue(nombreCmd, out cmdRet);
            return cmdRet;
        }
        public void AgregarComando(Comando c) // agregar uno
        {
            comandos.Add(c.Nombre,c);
        }
        public void AgregarComandos(Comando[] cs) // agregar muchos
        {
            foreach(Comando comando in cs)
            {
                comandos.Add(comando.Nombre, comando);
            }
        }
        public string[] GetNombreComandos() // listar los comandos (puede servir para el menu)
        {
            Comando[] cmds = comandos.Values.ToArray<Comando>();
            string[] result = new string[cmds.Length];
            for (int i = 0; i < cmds.Length; i++)
            {
                result[i] = cmds[i].Nombre;
            }
            return result;
        }
    }
}
