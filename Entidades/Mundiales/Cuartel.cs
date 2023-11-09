using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    class Cuartel
    {
        private string id;
        private Localizacion ubicacion;
        public string Identificacion()
        {
            return id;
        }

        public Localizacion GetUbicacion()
        {
            return ubicacion;
        }
    }
}
