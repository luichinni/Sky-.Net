﻿using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.CommandPattern.Comandos
{
    public class TransferirBateriaCmd : Comando
    {
        public TransferirBateriaCmd(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public override void Ejecutar(Mundo m, Cuartel c)
        {
            throw new NotImplementedException();
        }
    }
}
