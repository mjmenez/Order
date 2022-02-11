using Evaluacion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Negocio
{
    public class NegEstatus
    {
        public static List<CatEstatus> _ListaEstatus = new List<CatEstatus> { 
            new CatEstatus(){ IdCatEstatus = 1, Descripcion= "Pendiente", Activo = true},
            new CatEstatus(){ IdCatEstatus = 2, Descripcion= "En progreso", Activo = true},
            new CatEstatus(){ IdCatEstatus = 3, Descripcion= "Terminado", Activo = true},
            new CatEstatus(){ IdCatEstatus = 4, Descripcion= "Entregado", Activo = true},
            new CatEstatus(){ IdCatEstatus = 5, Descripcion= "Cancelado", Activo = true}
        };

        public CatEstatus ObtenerOrden(int Id)
        {
            return _ListaEstatus.Where(c => c.IdCatEstatus == Id).FirstOrDefault();
        }

        public string DescripcionEstatus(int Id)
        {
            CatEstatus catEstatus = ObtenerOrden(Id);
            return catEstatus.Descripcion ?? "";
        }
    }
}
