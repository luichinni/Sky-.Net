using SkyNet.Entidades.Mundiales;
using SkyNet.Entidades.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Entidades.Fabricas
{
    public abstract class Fabrica
    {
        public EnumOperadores Tipo {  get; set; }
        public static int Id { get; set; } // id comun para todos los operadores, no por tipo porq pinto asi
        public abstract Operador FabricarOperador(Cuartel c);
        public string GenerarId(EnumOperadores tipo,Cuartel c)
        {
            Id++;
            return tipo.ToString()+"-"+Id+"-"+c.Id; // ej: K9-17-SDHA; M8-5-DFRT; UAV-69-UWGP
        }
    }
}
