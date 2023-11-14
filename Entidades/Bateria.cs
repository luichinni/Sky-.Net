﻿using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades
{
    public class Bateria
    {
        private double bateriaMax, bateriaActual;

        public Bateria()
        {

        }

        public double ConsultarBateria()
        {
            return bateriaActual;
        }

        public double GetBateriaMax()
        {
            return bateriaMax;
        }


        public void CargarBateria(double cantBateria)
        {

            if (bateriaActual + cantBateria <= bateriaMax)
            {
                bateriaActual += cantBateria;
            }

        }

        public void LlenarBateria()
        {
            bateriaActual = bateriaMax;
        }


        public void ConsumirBateria(double cantBateria)
        {

            if (bateriaActual - cantBateria > 0)
            {
                bateriaActual -= cantBateria;
            }
        }

        public void InicializarBateria(double cantBateria)
        {
            bateriaMax = cantBateria;
            bateriaActual = cantBateria;
        }
    }
}
