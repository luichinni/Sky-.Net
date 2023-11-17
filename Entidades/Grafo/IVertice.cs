using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public interface IVertice <T>
    {
        public T GetDato();
        public void SetDato(T dato);
        public int GetPosicion();
        public void SetPosicion(int pos);
        public void Conectar(IVertice<T> vertice);
        public void Conectar(IVertice<T> vertice, int peso);
        public void Desconectar(IVertice<T> vertice);
        public int GetPeso(IVertice<T> vertice);
    }
}
