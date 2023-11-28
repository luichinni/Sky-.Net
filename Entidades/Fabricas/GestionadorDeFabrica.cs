using SkyNet.CommandPattern;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Fabricas
{
    public class GestionadorDeFabrica
    {
        Dictionary<EnumOperadores, Fabrica> fabricas; // uso diccionario para mejorar busqueda
        public GestionadorDeFabrica(Dictionary<EnumOperadores, Fabrica> fabricas = null)
        {
            if (fabricas != null) this.fabricas = new Dictionary<EnumOperadores, Fabrica>(fabricas);
            else this.fabricas = new Dictionary<EnumOperadores, Fabrica>();
        }
        public Fabrica GetFabrica(EnumOperadores tipo) // obtener comando
        {
            Fabrica fabricaRet;
            if (!fabricas.TryGetValue(tipo, out fabricaRet)) fabricaRet = null;
            return fabricaRet;
        }
        public void AgregarComando(Fabrica f) // agregar uno
        {
            fabricas.Add(f.Tipo, f);
        }
        public void AgregarComandos(Fabrica[] fs) // agregar muchos
        {
            foreach (Fabrica fabrica in fs)
            {
                fabricas.Add(fabrica.Tipo, fabrica);
            }
        }
    }
}
