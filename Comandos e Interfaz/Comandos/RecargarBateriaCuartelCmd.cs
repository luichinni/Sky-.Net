﻿using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class RecargarBateriaCuartelCmd : Comando
    {
        public RecargarBateriaCuartelCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, ref Cuartel c)
        {
            throw new NotImplementedException();
        }
    }
}