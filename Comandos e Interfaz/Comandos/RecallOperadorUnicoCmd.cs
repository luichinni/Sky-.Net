using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.CommandPattern;
using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;

namespace SkyNet.CommandPattern.Comandos
{
    public class RecallOperadorUnicoCmd : Comando
    {
        Menu _menu;
        string _cancelar = "Cancelar";
        public RecallOperadorUnicoCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            string[] operadores = new string[] { _cancelar };
            _menu = new MenuConcreto(operadores, nombre);
        }
        string[] ActualizarOperadores(Cuartel c)
        {
            return (c.Operadores.Count > 0) ? c.Operadores.Select(op => op.Id.ToString()).Append(_cancelar).ToArray() : _menu.Opciones;
        }
        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            _menu.Opciones = ActualizarOperadores(c);
            _menu.Mostrar();
            string idSelect = _menu.GetSeleccion();
            if (idSelect != _cancelar)
            {
                Operador o = c.Operadores.Find(op => op.Id == idSelect);
                c.RecallOperadorUnico(o);
            }
        }
    }
}
