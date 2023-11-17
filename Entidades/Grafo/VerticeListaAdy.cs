using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public class VerticeListaAdy<T> : IVertice<T>
    {
        private T dato;
        private int posicion;
        private List<IArista<T>> adyacentes;
        public VerticeListaAdy(T dato)
        {
            this.adyacentes = new List<IArista<T>>();
            this.dato = dato;
        }
        public void SetDato(T dato)
        {
            this.dato = dato;
        }
        public void Conectar(IVertice<T> vertice)
        {
            IArista<T> arista = ObtenerArista(vertice);
            if (arista == null)
            {
                adyacentes.Add(new AristaImpl<T>(vertice, 1));
            }
        }

        public void Conectar(IVertice<T> vertice, int peso)
        {
            IArista<T> arista = ObtenerArista(vertice);
            if (arista == null)
            {
                adyacentes.Add(new AristaImpl<T>(vertice, peso));
            }
        }

        public void Desconectar(IVertice<T> vertice)
        {
            IArista<T> arista = ObtenerArista(vertice);
            if(arista != null)
            {
                adyacentes.Remove(arista);
            }
        }

        public T GetDato()
        {
            return dato;
        }

        public int GetPeso(IVertice<T> destino)
        {
            IArista<T> arista = ObtenerArista(destino);
            int ret = -1; // -1 significa q no encuentra el peso
            if (arista != null) ret = arista.GetPeso();
            return ret;
        }
        public IArista<T> ObtenerArista(IVertice<T> destino)
        {
            IArista<T> arista = null;
            int indice = 0;
            while (arista == null && indice < adyacentes.Count)
            {
                if (adyacentes[indice].GetVerticeDestino() == destino)
                    arista = adyacentes[indice];
                indice++;
            }
            return arista;
        }

        public int GetPosicion()
        {
            return posicion;
        }

        public void SetPosicion(int pos)
        {
            posicion = pos;
        }
        public bool EsAdyacente(IVertice<T> vertice)
        {
            bool esta = false;
            int indice = 0;
            while(!esta && indice < adyacentes.Count)
            {
                if (adyacentes[indice].GetVerticeDestino() == vertice)
                    esta = true;
                indice++;
            }
            return esta;
        }
        public List<IArista<T>> GetAdyacentes()
        {
            return new List<IArista<T>>(adyacentes);
        }
    }
}
