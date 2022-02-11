using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Modelo
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public int  IdCatEstatus { get; set; }
        public int Folio { get; set; }
       // public int Cantidad { get; set; }

        //public virtual List<Producto> Productos { get; set; }
        //public virtual List<CatEstatus> CatEstatus { get; set; }
    }
}
