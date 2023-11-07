using SkyNet.Entidades.Fabricas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Mundiales
{
    class Mundo
    {
        static Mundo mundo;
        private Dictionary<string,Localizacion> mapamundi;
        private Mundo() { mapamundi = new Dictionary<string, Localizacion>(); }
        private Fabrica fabrica;
        public static Mundo GetInstance() 
        {
            if(mundo == null)
            {
                mundo = new Mundo();
            }
            return mundo;
        }
        public Fabrica ContactarFabrica()
        {
            return fabrica;
        }
        public Cuartel GetCuartel(int x, int y) 
        {
            Localizacion l; 
            mapamundi.TryGetValue($"{x}{y}", out l);
            return l.GetCuartel();
        }
        public Localizacion GetLocalizacion(int x,int y)
        {
            Localizacion localizacionRet;
            if (!mapamundi.ContainsKey($"{x}{y}"))
            {
                localizacionRet = new Localizacion(x,y);
                mapamundi.Add($"{x}{y}", localizacionRet);
            }
            else
            {
                mapamundi.TryGetValue($"{x}{y}", out localizacionRet);
            }
            return localizacionRet;
        }
    }
}
