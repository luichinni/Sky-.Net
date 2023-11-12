using SkyNet.Entidades.Fabricas;
using SkyNet.Entidades.Grafo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    class Mundo
    {
        private static double _porcentajeVertederos = 0.1;// 10% de las localizaciones libres son vertederos
        public static int PorcentajeDeVertederos
        {
            get => (int)_porcentajeVertederos * 100;
            set
            {
                if (value <= 100 || value >= 0)
                    _porcentajeVertederos = value/100.0;
            }
        }
        public static int MaxCoordX { get; set; } = 10;
        public static int MaxCoordY { get; set; } = 10;
        public static int MaxSitiosReciclaje { get; set; } = 5;
        static Mundo instancia;
        //private IGrafo<Localizacion> mundo;
        private Dictionary<string,IVertice<Localizacion>> mapamundi;
        private Mundo() 
        { 
            mapamundi = new Dictionary<string, IVertice<Localizacion>>();
            GenerarMundo();
        }
        private void GenerarMundo()
        {
            // Generamos todo el mundo como baldio,planicie,bosque o sector urbano (luego se agrega lo que falta)
            Random rnd = new Random();
            int n;
            EnumTiposDeZona zona;
            for (int i = 0; i < MaxCoordX; i++)
            {
                for(int j = 0; j < MaxCoordY; j++)
                {
                    n = rnd.Next(8);
                    if (n == 0) zona = EnumTiposDeZona.SectorUrbano;
                    else if (n == 1 || n == 2) zona = EnumTiposDeZona.TerrenoBaldio;
                    else if (n >= 3 && n < 5) zona = EnumTiposDeZona.Bosque;
                    else zona = EnumTiposDeZona.Planicie;
                    IVertice<Localizacion> v = new VerticeListaAdy<Localizacion>(new Localizacion(i, j,zona));
                    //mundo.AgregarVertice(v);
                    mapamundi.Add($"{i}{j}", v);
                }
            }
            // conectamos todo el grafo
            IVertice<Localizacion> origen;
            IVertice<Localizacion> destino;
            for (int j = 0;j < MaxCoordY-1; j++) // conexion vertical
            {
                for (int i = 0; i < MaxCoordX; i++)
                {
                    mapamundi.TryGetValue($"{i}{j}", out origen);
                    mapamundi.TryGetValue($"{i}{j+1}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
            for (int i = 0; i < MaxCoordX - 1; i++) // conexion horizontal
            {
                for (int j = 0; j < MaxCoordY; j++)
                {
                    mapamundi.TryGetValue($"{i}{j}", out origen);
                    mapamundi.TryGetValue($"{i+1}{j}", out destino);
                    origen.Conectar(destino);
                    destino.Conectar(origen);
                }
            }
            // generamos los puntos de reciclaje
            GenerarPuntosDeReciclaje();
            // generamos vertederos
            GenerarVertederos();
        }
        private void GenerarVertederos()
        {
            Random rnd = new Random();
            int cantVertederos = (int)(ContarLocalizacionesLibres() * _porcentajeVertederos);
            IVertice<Localizacion> vertice;
            for (int i = 0; i < cantVertederos ; i++)
            {
                mapamundi.TryGetValue($"{rnd.Next(MaxCoordX)}{rnd.Next(MaxCoordY)}", out vertice);
                if ((int)vertice.GetDato().GetTipoZona() <= 2)
                {
                    if (rnd.Next(2) == 1) vertice.GetDato().SetTipoZona(EnumTiposDeZona.Vertedero);
                    else vertice.GetDato().SetTipoZona(EnumTiposDeZona.VertederoElectronico);
                }
            }
        }
        private int ContarLocalizacionesLibres()
        {
            int contador = 0;
            foreach(KeyValuePair<string,IVertice<Localizacion>> vl in mapamundi)
            {
                if ((int)vl.Value.GetDato().GetTipoZona() <= 2) contador++;
            }
            return contador;
        }
        private void GenerarPuntosDeReciclaje()
        {
            Random rnd = new Random();
            int cantReciclajes = rnd.Next(MaxSitiosReciclaje) + 1;
            IVertice<Localizacion> vertice;
            for (int i = 0; i < cantReciclajes; i++)
            {
                mapamundi.TryGetValue($"{rnd.Next(MaxCoordX)}{rnd.Next(MaxCoordY)}", out vertice);
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
            mapamundi.TryGetValue($"{x}{y}", out i);
            return i.GetDato().GetCuartel();
        }
        public Localizacion GetLocalizacion(int x,int y)
        {
            IVertice<Localizacion> localizacionRet;
            mapamundi.TryGetValue($"{x}{y}", out localizacionRet);
            return localizacionRet.GetDato();
        }
    }
}
