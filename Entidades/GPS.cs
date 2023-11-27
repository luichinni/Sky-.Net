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
            /// Inicializando las listas de nodos a buscar y nodos cerrados a la busqueda
            VerticeListaAdy<Localizacion> nodoInicial = (VerticeListaAdy<Localizacion>)mundo.GetVertice(origen.coordX, origen.coordY);
            List<IVertice<Localizacion>> nodosAbiertos = new List<IVertice<Localizacion>>() { nodoInicial };
            List<IVertice<Localizacion>> nodosCerrados = new List<IVertice<Localizacion>>();
            Dictionary<int, VerticeListaAdy<Localizacion>> nodos = new Dictionary<int, VerticeListaAdy<Localizacion>>();
            for (int x=0; x<mundo.MaxCoordX; x++)
            {
                for (int y=0; y<mundo.MaxCoordY; y++)
                { /// Inicializacion de todos los nodos con costo alto para permitir mejora
                    VerticeListaAdy<Localizacion> nodo = (VerticeListaAdy<Localizacion>) mundo.GetVertice(x, y);
                    nodo.gCost = int.MaxValue;
                    nodo.CalcularFCost();
                    nodo.anterior = null;
                    nodos.Add(nodo.fCost,nodo);
                }
            }

            nodoInicial.gCost = 0; // costo de movimiento
            nodoInicial.hCost = CalcularDistancia(nodoInicial.GetDato(), destino); // costo estimado de movimiento
            nodoInicial.CalcularFCost(); // suma de los anteriores
            List<Localizacion> caminoRet = null;
            while (nodosAbiertos.Count > 0 && caminoRet == null)
            {
                VerticeListaAdy<Localizacion> nodoActual = GetNodoMenorFCost(nodos);
                if (nodoActual == mundo.GetVertice(destino.coordX, destino.coordY))
                { /// si es el final recuperamos el camino
                    caminoRet = CalcularCamino(nodoActual);
                }
                else if (zonasProhibidas.Contains(nodoActual.GetDato().TipoZona))
                { /// si es un nodo prohibido lo ignoramos
                    nodosCerrados.Add(nodoActual);
                }
                else
                {
                    nodosAbiertos.Remove(nodoActual);
                    nodosCerrados.Add(nodoActual);

                    foreach (VerticeListaAdy<Localizacion> v in mundo.GetGrafo().ListaDeAdyacentes(nodoActual))
                    {
                        if (!nodosCerrados.Contains(v))
                        {
                            int GCostTentativo = nodoActual.gCost + CalcularDistancia(nodoActual.GetDato(), v.GetDato());
                            if (GCostTentativo < v.gCost) /// se intenta mejorar el costo de camino del nodo
                            {
                                v.anterior = nodoActual;
                                v.gCost = GCostTentativo;
                                v.hCost = CalcularDistancia(v.GetDato(), destino);
                                v.CalcularFCost();

                                if (!nodosAbiertos.Contains(v)) nodosAbiertos.Add(v);
                            }
                        }
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
            while (verticeActual.anterior != null) 
            {
                listaRet.Add(verticeActual.anterior.GetDato());
                verticeActual = verticeActual.anterior;
            }
            listaRet.Reverse();
            return listaRet;
        }
        private VerticeListaAdy<Localizacion> GetNodoMenorFCost(Dictionary<int, VerticeListaAdy<Localizacion>> nodos)
        {
            VerticeListaAdy<Localizacion> menorFCostNodo = nodos[0];
            for (int i=0; i<nodos.Count; i++)
            {
                if (nodos[i].fCost < menorFCostNodo.fCost)
                {
                    menorFCostNodo = nodos[i];
                }
            }
            return menorFCostNodo;
        }
        public int CalcularDistancia(Localizacion origen,Localizacion destino)
        { // este metodo lo hizo christian, yo solo lo movi de clase
            int distancia = 0;

            distancia += Math.Abs((origen.coordX - destino.coordX));

            distancia += Math.Abs(origen.coordY - destino.coordY);

            return distancia;
        }

        public Localizacion BuscarCercano(EnumTiposDeZona zona, Localizacion origen)
        {

            return null;
        }
    }
}
