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
    public class AgregarReservaCmd : Comando
    {
        Menu _menu;
        string _cancelar = "Cancelar";
        public AgregarReservaCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
            string[] operadores = new string[] { _cancelar };
            _menu = new MenuConcreto(operadores,nombre);
        }
        string[] ActualizarOperadores(Cuartel c)
        {
            return (c.Operadores.Count > 0) ? c.Operadores.Select(op => op.Id.ToString()).Append(_cancelar).ToArray() : _menu.Opciones;
        }
        public override bool Ejecutar(Mundo m, ref Cuartel c)
        {
            _menu.Opciones = ActualizarOperadores(c);
            _menu.Mostrar();
            string idSelect = _menu.GetSeleccion();
            if (idSelect != _cancelar)
            {
                Operador o = c.Operadores.Find(op => op.Id == idSelect);
                c.RecallOperadorUnico(o);
                if (o.getUbicacion() == c.GetUbicacion())
                {
                    o.cambiarEstado(EnumEstadoOperador.Reserva);
                    ConsoleHelper.EscribirCentrado($"{o.Id} fue agregado a la reserva, estado:{o.Estado}");
                }
                else
                {
                    ConsoleHelper.EscribirCentrado($"{o.Id} no pudo llegar a cuartel, posicion actual: x{o.CoordX} y{o.CoordY}");
                }
                
            }
            return true;
        }
    }
}
