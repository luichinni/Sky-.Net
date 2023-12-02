using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Grafo
{
    public interface IGrafo <T>
    {
        public void AgregarVertice(IVertice<T> vertice);
        public void EliminarVertice(IVertice<T> vertice);
        public void Conectar(IVertice<T> origen, IVertice<T> destino);
        public void Desconectar(IVertice<T> origen, IVertice<T> destino);
        public bool EsAdyacente(IVertice<T> origen, IVertice<T> destino);
        public List<IVertice<T>> ListaDeVertices();
        public List<IVertice<T>> ListaDeAdyacentes(IVertice<T> vertice);
        public IVertice<T> GetVertice(int indiceVertice);
        public bool EsVacio();
    }
}
