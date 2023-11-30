using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class TransferirCargaFisicaCmd : Comando
    {
        Menu _menu;
        string _cancelar = "Cancelar";
        public TransferirCargaFisicaCmd(string nombre, string descripcion) : base(nombre, descripcion)
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
            ConsoleHelper.EscribirCentrado("Primer seleccion es el receptor, segunda el donante");
            string idSelect = _menu.GetSeleccion();
            List<string> listadoAux = _menu.Opciones.ToList();
            listadoAux.Remove(idSelect);
            _menu.Opciones = listadoAux.ToArray();
            string idSelect2 = _menu.GetSeleccion();

            if (idSelect != _cancelar)
            {
                Operador receptor = c.Operadores.Find(op => op.Id == idSelect);
                Operador donante = c.Operadores.Find(op => op.Id == idSelect2);
                if (receptor.getUbicacion() == donante.getUbicacion())
                {
                    double cargaAntes = receptor.CargaActual;
                    donante.TransferirCargaFisica(receptor, receptor.CargaMax);
                    ConsoleHelper.EscribirCentrado($"{receptor.Id} recibio {receptor.CargaActual - cargaAntes}kg de carga");
                }
                else
                {
                    ConsoleHelper.EscribirCentrado($"Los operadores no se encuentran en la misma ubicacion, pos receptor: x{receptor.CoordX} y{receptor.CoordY}");
                }

            }
        }
    }
}
