using SkyNet.Entidades.Fabricas;
using SkyNet.Entidades.Grafo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    public class Mundo
    {
        public int[] PrioridadZonal { get; set; } = new int[Enum.GetNames(typeof(EnumTiposDeZona)).Length];
        public int[] ExtensionZonal { get; set; } = new int[Enum.GetNames(typeof(EnumTiposDeZona)).Length];
        public int[] ExpansionZonal { get; set; } = new int[Enum.GetNames(typeof(EnumTiposDeZona)).Length];
        public int[] MaximaAparicion { get; set; } = new int[Enum.GetNames(typeof(EnumTiposDeZona)).Length];
        public int MaxCoordX { get; set; } = 100; // maximo tamaño en x
        public int MaxCoordY { get; set; } = 100; // maximo tamaño en y
        public int MaxCuartels { get; set; } = 3;
        public int CantCuarteles { get; set; } = 0;
        static Mundo instancia;

        public IGrafo<Localizacion> GrafoMundo { get; set; }
        public Dictionary<string, IVertice<Localizacion>> Mapamundi { get; set; } // vertices del mundo
        public IVertice<Localizacion> GetVertice(int x, int y)
        {
            Mapamundi.TryGetValue($"x{x}y{y}", out IVertice<Localizacion> VRet);
            return VRet;
        }
        private Mundo()
        {
        }
        public void IniciarSimulacion()
        {
            GenerarMundo();
        }
        public void ReanudarSimulacion(Dictionary<string,Localizacion> mundoReanudado)
        {
            InicializarGrafo();
            ConectarUbicacionesEnElGrafo();
            for (int i = 0; i < MaxCoordX; i++)
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    Localizacion loc;
                    mundoReanudado.TryGetValue($"x{i}y{j}",out loc);
                    Mapamundi.TryGetValue($"x{i}y{j}", out IVertice<Localizacion> ver);
                    ver.SetDato(loc);
                }
            }
        }
        private void GenerarMundo()
        {
            InicializarGrafo();
            ConectarUbicacionesEnElGrafo();
            InicializarMundoMundial();
        }
        private void InicializarGrafo()
        {
            Mapamundi = new Dictionary<string, IVertice<Localizacion>>();
            GrafoMundo = new GrafoImplListAdy<Localizacion>();
            for (int i = 0; i < MaxCoordX; i++)
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    // la zona con prioridad 0 es la base del mundo
                    IVertice<Localizacion> v = new VerticeListaAdy<Localizacion>(new Localizacion(i, j, (EnumTiposDeZona)PrioridadZonal[0]));
                    GrafoMundo.AgregarVertice(v);
                    Mapamundi.Add($"x{i}y{j}", v);
                }
            }
        }
        private void InicializarMundoMundial()
        {
            Queue<IVertice<Localizacion>> colaZonal;
            Random rnd = new Random();
            List<IVertice<Localizacion>> disponibles = Mapamundi.Values.ToList();
            HashSet<IVertice<Localizacion>> visitados = new HashSet<IVertice<Localizacion>>();
            for (int i = 0; i < ExpansionZonal.Length; i++) // Para cada zona
            {
                for (int j = 0; j < MaximaAparicion[PrioridadZonal[i]]; j++) // Mientras tenga aparicion
                {
                    int expansion = ExpansionZonal[PrioridadZonal[i]]; // tomamos cuanto debe extenderse
                    IVertice<Localizacion> verticeActual;
                    int indiceSig = rnd.Next(disponibles.Count); // tomamos una localizacion inicial disponible al azar
                    verticeActual = disponibles[indiceSig];
                    disponibles.RemoveAt(indiceSig); // la quitamos de las disponibles
                    // obtenemos la extension inicial de la zona
                    colaZonal = GetExtensionNucleoZona(ExtensionZonal[PrioridadZonal[i]], verticeActual, visitados);
                    colaZonal.Enqueue(null); // separador de nivel
                    while (colaZonal.Count > 0 && expansion > 0) // mientras haya localizaciones por iniciar
                    {
                        verticeActual = colaZonal.Dequeue(); // la sacamos de la cola
                        if(verticeActual != null) // si no es un separador de nivel
                        {
                            verticeActual.GetDato().TipoZona = (EnumTiposDeZona)PrioridadZonal[i]; // establecemos el tipo de zona correspondiente
                            foreach (IArista<Localizacion> arista in GrafoMundo.ListaDeAdyacentes(verticeActual))
                            { // para cada lugar adyacente, si no fue visitado y la expansion no llega al final,
                              // lo encola para luego procesarlo y lo visita para bloquearlo
                                if (!visitados.Contains(arista.GetVerticeDestino()) && expansion > 1)
                                {
                                    colaZonal.Enqueue(arista.GetVerticeDestino());
                                    visitados.Add(arista.GetVerticeDestino());
                                }
                                    
                            }
                        }
                        else
                        { // si se termina un nivel, se disminuye la expansion y se encola otro separador
                            expansion--;
                            colaZonal.Enqueue(null);
                        }
                    }
                    
                }
                

            }
        }
        private Queue<IVertice<Localizacion>> GetExtensionNucleoZona(int extension,IVertice<Localizacion> vertice, HashSet<IVertice<Localizacion>> visitados)
        {
            Queue<IVertice<Localizacion>> cola;
            /// Se genera el nucleo lineal del tipo de zona a partir de un algoritmo DFS
            if (extension > 0)
            {
                Random rnd = new Random();
                /// Agregamos el vertice actual a lista de visitados
                visitados.Add(vertice);

                /// Buscamos siguiente vertice, si ya fue visitado se busca otro, en caso de 
                /// que todos los adyacentes hayan sido visitados, se deja de buscar y se retorna
                /// directamente lo que se tiene hasta el momento
                List<IArista<Localizacion>> adyacentes = GrafoMundo.ListaDeAdyacentes(vertice);
                int indice = rnd.Next(adyacentes.Count-1);// le restrinjo un lado para evitar cuadrados?
                IVertice<Localizacion> siguiente = adyacentes[indice].GetVerticeDestino();
                int intentos = 1;
                while (visitados.Contains(siguiente) && intentos < adyacentes.Count) 
                {
                    if (indice < adyacentes.Count-1) indice++;
                    else indice = 0;
                    adyacentes[indice].GetVerticeDestino();
                    intentos++;
                }

                /// Recibimos la cola del resto del dfs y guardamos nuestro vertice, en caso de no poder
                /// seguir creamos la cola
                if (visitados.Contains(siguiente)) cola = new Queue<IVertice<Localizacion>>();
                else cola = GetExtensionNucleoZona(extension - 1, siguiente, visitados);
                cola.Enqueue(vertice);
            }
            else
            {
                cola = new Queue<IVertice<Localizacion>>();
                cola.Enqueue(vertice);
            }
            return cola;
        }
        private void ConectarUbicacionesEnElGrafo()
        {
            IVertice<Localizacion> origen;
            IVertice<Localizacion> destino;
            /// ejemplo de grafo cuadrado
            /// [0,0] [1,0] [2,0] [3,0] [4,0]
            /// [0,1] [1,1] [2,1] [3,1] [4,1]
            /// [0,2] [1,2] [2,2] [3,2] [4,2]
            /// [0,3] [1,3] [2,3] [3,3] [4,3]
            /// [0,4] [1,4] [2,4] [3,4] [4,4]
            /// 
            /// la primer conexion establece verticalidad
            /// [0,0] [1,0] [2,0] [3,0] [4,0]
            ///  ↓↑    ↓↑    ↓↑    ↓↑    ↓↑
            /// [0,1] [1,1] [2,1] [3,1] [4,1]
            ///  ↓↑    ↓↑    ↓↑    ↓↑    ↓↑
            /// [0,2] [1,2] [2,2] [3,2] [4,2]
            ///  ↓↑    ↓↑    ↓↑    ↓↑    ↓↑
            /// [0,3] [1,3] [2,3] [3,3] [4,3]
            ///  ↓↑    ↓↑    ↓↑    ↓↑    ↓↑
            /// [0,4] [1,4] [2,4] [3,4] [4,4]
            /// 
            /// la segunda conexion establece horizontalidad
            /// [0,0] <-> [1,0] <-> [2,0] <-> [3,0] <-> [4,0]
            /// [0,1] <-> [1,1] <-> [2,1] <-> [3,1] <-> [4,1]
            /// [0,2] <-> [1,2] <-> [2,2] <-> [3,2] <-> [4,2]
            /// [0,3] <-> [1,3] <-> [2,3] <-> [3,3] <-> [4,3]
            /// [0,4] <-> [1,4] <-> [2,4] <-> [3,4] <-> [4,4]
            /// 
            /// esto permite que todo el grafo sea bidireccional y todas
            /// las ubicaciones conozcan a sus adyacentes
            for (int j = 0; j < MaxCoordY - 1; j++) // conexion vertical
            {
                for (int i = 0; i < MaxCoordX; i++)
                {
                    Mapamundi.TryGetValue($"x{i}y{j}", out origen);
                    Mapamundi.TryGetValue($"x{i}y{j + 1}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
            for (int i = 0; i < MaxCoordX - 1; i++) // conexion horizontal
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    Mapamundi.TryGetValue($"x{i}y{j}", out origen);
                    Mapamundi.TryGetValue($"x{i + 1}y{j}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
        }
        
        private Fabrica fabrica;
        public static Mundo GetInstance() 
        {
            if(instancia == null)
            {
                instancia = new Mundo();
            }
            return instancia;
        }
        public Fabrica ContactarFabrica()
        {
            return fabrica;
        }
        public Cuartel GetCuartel(int x, int y) 
        {
            IVertice<Localizacion> i; 
            Mapamundi.TryGetValue($"x{x}y{y}", out i);
            return i.GetDato().GetCuartel();
        }
        public Localizacion GetLocalizacion(int x,int y)
        {
            IVertice<Localizacion> localizacionRet;
            Mapamundi.TryGetValue($"x{x}y{y}", out localizacionRet);
            return localizacionRet.GetDato();
        }
    }
}
