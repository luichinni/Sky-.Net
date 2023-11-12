using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public class AristaImpl<T> : IArista<T>
    {
        private IVertice<T> destino;
        private int peso;
        public AristaImpl(IVertice<T> destino, int peso)
        {
            this.destino = destino;
            this.peso = peso;
        }

        public int GetPeso()
        {
            return peso;
        }

        public IVertice<T> GetVerticeDestino()
        {
            return destino;
        }
    }
}
