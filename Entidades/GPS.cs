using SkyNet.CommandPattern;
using SkyNet.Entidades.Grafo;
using SkyNet.Entidades.Mundiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades
{
    public class GPS
    {
        private Mundo mundo;
        public GPS() 
        {
            mundo = Mundo.GetInstance();
        }
        public List<Localizacion> GetCamino(Localizacion origen, Localizacion destino, EnumTiposDeZona[] zonasProhibidas)
        {
            EnumTiposDeZona[] zonasProhibidasCopy;
            if (zonasProhibidas.Contains(origen.TipoZona))
            {
                List<EnumTiposDeZona> listAux = zonasProhibidas.ToList();
                listAux.Remove(origen.TipoZona);
                zonasProhibidasCopy = listAux.ToArray(); // esto es porq sino queda atrapado cuando está en el vertedero
            }
            else zonasProhibidasCopy = zonasProhibidas;
            /// Inicializando las listas de nodos a buscar y nodos cerrados a la busqueda
            VerticeListaAdy<Localizacion> nodoInicial = (VerticeListaAdy<Localizacion>)mundo.GetVertice(origen.coordX, origen.coordY);
            
            HashSet<VerticeListaAdy<Localizacion>> nodosAbiertos = new HashSet<VerticeListaAdy<Localizacion>>() { nodoInicial };
            HashSet<VerticeListaAdy<Localizacion>> nodosCerrados = new HashSet<VerticeListaAdy<Localizacion>>();
            
            InicializarVisitados(mundo.Mapamundi.Values.ToList());
            for (int x=0; x<mundo.MaxCoordX; x++)
            {
                for (int y=0; y<mundo.MaxCoordY; y++)
                { /// Inicializacion de todos los nodos con costo alto para permitir mejora
                    VerticeListaAdy<Localizacion> nodo = (VerticeListaAdy<Localizacion>) mundo.GetVertice(x, y);
                    nodo.gCost = int.MaxValue;
                    nodo.CalcularFCost();
                }
            }

            nodoInicial.gCost = 0; // costo de movimiento
            nodoInicial.hCost = CalcularDistancia(nodoInicial.GetDato(), destino); // costo estimado de movimiento
            nodoInicial.CalcularFCost(); // suma de los anteriores
            List<Localizacion> caminoRet = null;
            while (nodosAbiertos.Count > 0 && caminoRet == null)
            {
                VerticeListaAdy<Localizacion> nodoActual = GetNodoMenorFCost(nodosAbiertos);
                if (nodoActual == mundo.GetVertice(destino.coordX, destino.coordY))
                { /// si es el final recuperamos el camino
                    caminoRet = CalcularCamino(nodoActual);
                }
                else if (zonasProhibidasCopy.Contains(nodoActual.GetDato().TipoZona))
                { /// si es un nodo prohibido lo ignoramos
                    nodosCerrados.Add(nodoActual);
                    nodosAbiertos.Remove(nodoActual);
                }
                else
                {
                    nodosAbiertos.Remove(nodoActual);
                    nodosCerrados.Add(nodoActual);
                    foreach (IArista<Localizacion> v in mundo.GrafoMundo.ListaDeAdyacentes(nodoActual))
                    {
                        VerticeListaAdy<Localizacion> vAux = (VerticeListaAdy<Localizacion>)v.GetVerticeDestino();
                        if (nodosCerrados.Contains(vAux)) continue; // si está en la lista pasa al siguiente
                        int GCostTentativo = nodoActual.gCost + CalcularDistancia(nodoActual.GetDato(), vAux.GetDato());
                        if (!nodosAbiertos.Contains(vAux)) nodosAbiertos.Add(vAux);
                        else if (GCostTentativo >= vAux.gCost) continue;

                        vAux.anterior = nodoActual;
                        vAux.gCost = GCostTentativo;
                        vAux.hCost = CalcularDistancia(vAux.GetDato(), destino);
                        vAux.CalcularFCost();
                    }
                }
            }
            return caminoRet;
        }
        private List<Localizacion> CalcularCamino(VerticeListaAdy<Localizacion> final)
        {
            /// va desde el nodo final hasta el primero siguiendo la cadena generada en el algortimo A*,
            /// y luego invierte la lista para hacer el caminito bien
            List<Localizacion> listaRet = new List<Localizacion>();
            listaRet.Add(final.GetDato());
            VerticeListaAdy<Localizacion> verticeActual = final;
            while (verticeActual != null) 
            {
                listaRet.Add(verticeActual.GetDato());
                verticeActual = verticeActual.anterior;
            }
            listaRet.Reverse();
            return listaRet;
        }
        private VerticeListaAdy<Localizacion> GetNodoMenorFCost(HashSet<VerticeListaAdy<Localizacion>> nodos)
        {
            return nodos.OrderBy(nodito => nodito.fCost).FirstOrDefault();
        }
        public int CalcularDistancia(Localizacion origen,Localizacion destino)
        { // este metodo lo hizo christian, yo solo lo movi de clase
            int distancia = 0;

            distancia += Math.Abs((origen.coordX - destino.coordX));

            distancia += Math.Abs(origen.coordY - destino.coordY);

            return distancia;
        }
        private void InicializarVisitados(List<IVertice<Localizacion>> nodos)
        {
            for (int x = 0; x < mundo.MaxCoordX; x++)
            {
                for (int y = 0; y < mundo.MaxCoordY; y++)
                { /// Inicializacion de todos los nodos con costo alto para permitir mejora
                    VerticeListaAdy<Localizacion> nodo = (VerticeListaAdy<Localizacion>)mundo.GetVertice(x, y);
                    nodo.anterior = null;
                }
            }
        }
        public Localizacion BuscarCercano(EnumTiposDeZona zona, Localizacion origen)
        {
            Localizacion zonaRet = null;
            Queue<IVertice<Localizacion>> cola = new Queue<IVertice<Localizacion>>();
            List<IVertice<Localizacion>> disponibles = mundo.Mapamundi.Values.ToList();
            HashSet<IVertice<Localizacion>> visitados = new HashSet<IVertice<Localizacion>>();
            IVertice<Localizacion> verticeActual = disponibles.Find(vert => vert.GetDato() == origen);
            disponibles.Remove(verticeActual);
            cola.Enqueue(verticeActual);
            while (cola.Count > 0 && zonaRet == null) // mientras haya localizaciones por iniciar
            {
                verticeActual = cola.Dequeue(); // la sacamos de la cola
                if (verticeActual.GetDato().TipoZona == zona) zonaRet = verticeActual.GetDato();
                else
                {
                    foreach (IArista<Localizacion> arista in mundo.GrafoMundo.ListaDeAdyacentes(verticeActual))
                    { // para cada lugar adyacente, si no fue visitado y la expansion no llega al final,
                      // lo encola para luego procesarlo y lo visita para bloquearlo
                        if (!visitados.Contains(arista.GetVerticeDestino()))
                        {
                            cola.Enqueue(arista.GetVerticeDestino());
                            visitados.Add(arista.GetVerticeDestino());
                        }

                    }
                }
            }
            return zonaRet;
        }
    }
}
