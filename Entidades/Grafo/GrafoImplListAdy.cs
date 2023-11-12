using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public class GrafoImplListAdy<T> : IGrafo<T>
    {
        List<IVertice<T>> vertices = new List<IVertice<T>>();
        public void AgregarVertice(IVertice<T> vertice)
        {
            if (!vertices.Contains(vertice)) vertices.Add(vertice);
            ((VerticeListaAdy<T>) vertice).SetPosicion(vertices.Count-1);
        }

        public void Conectar(IVertice<T> origen, IVertice<T> destino)
        {
            origen.Conectar(destino);
        }

        public void Conectar(IVertice<T> origen, IVertice<T> destino, int peso)
        {
            origen.Conectar(destino,peso);
        }

        public void Desconectar(IVertice<T> origen, IVertice<T> destino)
        {
            origen.Desconectar(destino);
        }

        public void EliminarVertice(IVertice<T> vertice)
        {
            int indice = ((VerticeListaAdy<T>)vertice).GetPosicion();
            if (indice >= 0)
            {
                List<IArista<T>> aristasAdy = ListaDeAdyacentes(vertice);
                foreach (IArista<T> arista in aristasAdy)
                {
                    arista.GetVerticeDestino().Desconectar(vertice);
                }
                vertices.Remove(vertice);
            }
        }

        public bool EsAdyacente(IVertice<T> origen, IVertice<T> destino)
        {
            return ((VerticeListaAdy<T>)origen).EsAdyacente(destino);
        }

        public bool EsVacio()
        {
            return vertices.Count == 0;
        }

        public int GetPeso(IVertice<T> origen, IVertice<T> destino)
        {
            return origen.GetPeso(destino);
        }

        public IVertice<T> GetVertice(int indiceVertice)
        {
            return vertices[indiceVertice];
        }

        public List<IArista<T>> ListaDeAdyacentes(IVertice<T> vertice)
        {
            return ((VerticeListaAdy<T>)vertice).GetAdyacentes();
        }

        public List<IVertice<T>> ListaDeVertices()
        {
            return new List<IVertice<T>>(vertices);
        }
    }
}
