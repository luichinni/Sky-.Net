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
        private List<IVertice<T>> adyacentes = new List<IVertice<T>>();
        public int gCost, hCost, fCost;
        public VerticeListaAdy<T> anterior = null;
        public void CalcularFCost()
        {
            fCost = gCost + hCost;
        }
        public VerticeListaAdy(T dato)
        {
            this.dato = dato;
        }
        public void SetDato(T dato)
        {
            this.dato = dato;
        }
        public void Conectar(IVertice<T> vertice)
        {
            if (vertice != null && !adyacentes.Contains(vertice))
            {
                adyacentes.Add(vertice);
            }
        }

        public void Desconectar(IVertice<T> vertice)
        {
            if(vertice != null)
            {
                adyacentes.Remove(vertice);
            }
        }

        public T GetDato()
        {
            return dato;
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
                if (adyacentes[indice] == vertice)
                    esta = true;
                indice++;
            }
            return esta;
        }
        public List<IVertice<T>> GetAdyacentes()
        {
            return new List<IVertice<T>>(adyacentes);
        }
    }
}
