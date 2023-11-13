using SkyNet.Entidades.Fabricas;
using SkyNet.Entidades.Grafo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    public class Mundo
    {
        public static int PorcentajeVertederoNormal { get; set; } = 50; // 50% vertederos normales
        public static int PorcentajeVertederoElectronico { get; set; } = 50; // 50% vertederos electronicos
        public static int PorcentajeUrbanismo { get; set; } = 10;// 8% urbanismos
        public static int PorcentajeBaldios { get; set; } = 10;// 8% baldios
        public static int PorcentajeBosques { get; set; } = 30;// 30% bosques
        public static int PorcentajePlanicies { get; set; } = 50;// 50% planicies

        private static double _porcentajeAgua = 0.1;// 10% de las localizaciones libres son agua
        public static int PorcentajeDeAgua
        {
            get => (int)_porcentajeAgua * 100;
            set
            {
                if (value <= 100 || value >= 0)
                    _porcentajeAgua = value / 100.0;
            }
        }
        private static double _porcentajeVertederos = 0.08;// 8% de las localizaciones libres son vertederos
        public static int PorcentajeDeVertederos
        {
            get => (int)_porcentajeVertederos * 100;
            set
            {
                if (value <= 100 || value >= 0)
                    _porcentajeVertederos = value/100.0;
            }
        }
        public static int MaxCoordX { get; set; } = 10; // maximo tamaño en x
        public static int MaxCoordY { get; set; } = 10; // maximo tamaño en y
        public static int MaxSitiosReciclaje { get; set; } = 5; // cantidad maxima de sitios de reciclaje
        static Mundo instancia;
        //private IGrafo<Localizacion> mundo;
        private Dictionary<string,IVertice<Localizacion>> mapamundi; // vertices del mundo
        private Mundo() 
        { 
            //mapamundi = new Dictionary<string, IVertice<Localizacion>>();
            //GenerarMundo();
        }
        public void IniciarSimulacion()
        {
            mapamundi = new Dictionary<string, IVertice<Localizacion>>();
            GenerarMundo();
        }
        private void GenerarMundo()
        {
            // Generamos todo el mundo como baldio,planicie,bosque o sector urbano (luego se agrega lo que falta)
            InicializarMundoMundial();
            // conectamos todo el grafo
            ConectarUbicacionesEnElGrafo();
            // generamos los puntos de reciclaje
            GenerarPuntosDeReciclaje();
            // generamos vertederos
            GenerarVertederos();
            // generamos lagos
            GenerarLagos();
        }
        private void InicializarMundoMundial()
        {
            // Generamos todo el mundo como baldio,planicie,bosque o sector urbano (luego se agrega lo que falta)
            EnumTiposDeZona zona;
            for (int i = 0; i < MaxCoordX; i++)
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    zona = GetZonaSinDebuff();
                    IVertice<Localizacion> v = new VerticeListaAdy<Localizacion>(new Localizacion(i, j, zona));
                    //mundo.AgregarVertice(v);
                    mapamundi.Add($"x{i}y{j}", v);
                }
            }
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
                    mapamundi.TryGetValue($"x{i}y{j}", out origen);
                    mapamundi.TryGetValue($"x{i}y{j + 1}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
            for (int i = 0; i < MaxCoordX - 1; i++) // conexion horizontal
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    mapamundi.TryGetValue($"x{i}y{j}", out origen);
                    mapamundi.TryGetValue($"x{i + 1}y{j}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
        }
        private EnumTiposDeZona GetZonaSinDebuff()
        {
            EnumTiposDeZona zonaRet;
            Random rnd = new Random();
            /// Se suman todos los porcentajes de aparicion en tramos para poder mapearlos en el if
            /// el porcentaje total se randomiza para obtener un valor que sirva para mapear
            /// por ejemplo:
            ///     urbanismo = 10%
            ///     baldio = 10%
            ///     bosque = 40%
            ///     planicie = 50%
            /// si bien el resultado total es 110, el valor randomizado se hace sobre el 100% del total
            /// siendo 110 - 100%, supongamos que sale 57, dada la suma 10+10 = 20, 20 + 40 = 60, el 57
            /// corresponde al bioma de bosque.
            /// Nota: no es escalable a más biomas, tarea para casa resolver ese problema jaja
            int rangoUrbano_Baldio = PorcentajeUrbanismo + PorcentajeBaldios;
            int rangoBaldio_Bosque = rangoUrbano_Baldio + PorcentajeBosques;
            int porcentajesTotal = rangoBaldio_Bosque + PorcentajePlanicies;
            int n = rnd.Next(porcentajesTotal);
            if (n >= 0 && n < PorcentajeBaldios) zonaRet = EnumTiposDeZona.SectorUrbano;
            else if (n >= PorcentajeBaldios && n < rangoUrbano_Baldio) zonaRet = EnumTiposDeZona.TerrenoBaldio;
            else if (n >= rangoUrbano_Baldio && n < rangoBaldio_Bosque) zonaRet = EnumTiposDeZona.Bosque;
            else zonaRet = EnumTiposDeZona.Planicie;
            return zonaRet;
        }
        private void GenerarLagos()
        {
            /// Genera lagos a partir de las ubicaciones sin debuff multiplicado por el porcentaje maximo de aparicion
            /// luego aleatoriamente se intenta cubrir ese porcentaje (nada asegura que se llegue al maximo, solo lo intenta)
            Random rnd = new Random();
            int cantAgua = (int)(ContarLocalizacionesLibres() * _porcentajeAgua);
            IVertice<Localizacion> vertice;
            for (int i = 0; i < cantAgua; i++)
            {
                mapamundi.TryGetValue($"x{rnd.Next(MaxCoordX)}y{rnd.Next(MaxCoordY)}", out vertice);
                if ((int)vertice.GetDato().GetTipoZona() <= 3)
                {
                    vertice.GetDato().SetTipoZona(EnumTiposDeZona.Lago);
                }
            }
        }
        private void GenerarVertederos()
        {
            /// Genera vertederos a partir de las ubicaciones sin debuff multiplicado por el porcentaje maximo de aparicion
            /// luego aleatoriamente se intenta cubrir ese porcentaje (nada asegura que se llegue al maximo, solo lo intenta)
            /// también se selecciona con determinada probabilidad un tipo de vertedero o el otro
            Random rnd = new Random();
            int cantVertederos = (int)(ContarLocalizacionesLibres() * _porcentajeVertederos);
            int porcentajeVertederos = PorcentajeVertederoNormal + PorcentajeVertederoElectronico;
            IVertice<Localizacion> vertice;
            for (int i = 0; i < cantVertederos ; i++)
            {
                mapamundi.TryGetValue($"x{rnd.Next(MaxCoordX)}y{rnd.Next(MaxCoordY)}", out vertice);
                if ((int)vertice.GetDato().GetTipoZona() <= 2)
                {
                    int n = rnd.Next(porcentajeVertederos);
                    if (n < PorcentajeVertederoNormal) vertice.GetDato().SetTipoZona(EnumTiposDeZona.Vertedero);
                    else vertice.GetDato().SetTipoZona(EnumTiposDeZona.VertederoElectronico);
                }
            }
        }
        private int ContarLocalizacionesLibres()
        {
            /// Cuenta las localizaciones sin debuff que siempre están al comienzo del enum
            /// Tarea: hacer q ese 2 no este hardcodeado
            int contador = 0;
            foreach(KeyValuePair<string,IVertice<Localizacion>> vl in mapamundi)
            {
                if ((int)vl.Value.GetDato().GetTipoZona() <= 2) contador++;
            }
            return contador;
        }
        private void GenerarPuntosDeReciclaje()
        {
            /// Se genera un numero aleatorio de sitios de reciclaje basado en el numero maximo posible
            Random rnd = new Random();
            int cantReciclajes = rnd.Next(MaxSitiosReciclaje) + 1;
            IVertice<Localizacion> vertice;
            for (int i = 0; i < cantReciclajes; i++)
            {
                mapamundi.TryGetValue($"x{rnd.Next(MaxCoordX)}y{rnd.Next(MaxCoordY)}", out vertice);
                vertice.GetDato().SetTipoZona(EnumTiposDeZona.SitioReciclaje);
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
            mapamundi.TryGetValue($"x{x}y{y}", out i);
            return i.GetDato().GetCuartel();
        }
        public Localizacion GetLocalizacion(int x,int y)
        {
            IVertice<Localizacion> localizacionRet;
            mapamundi.TryGetValue($"x{x}y{y}", out localizacionRet);
            return localizacionRet.GetDato();
        }
    }
}
