using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public interface IArista <T>
    {
        public IVertice<T> GetVerticeDestino();
        public int GetPeso();
    }
}
