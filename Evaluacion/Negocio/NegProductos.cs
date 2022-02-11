using Evaluacion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Negocio
{
    public class NegProductos
    {
        public static List<Producto> _ListaProductos = new List<Producto>
        {
            new Producto (){IdProducto = 1, Descripcion = "Hamburguesa", CantidadAlmacen = 3, Activo = true},
            new Producto (){IdProducto = 2, Descripcion = "Papas", CantidadAlmacen = 8, Activo = true},
            new Producto (){IdProducto = 3, Descripcion = "Refresco", CantidadAlmacen = 5, Activo = true},
            new Producto (){IdProducto = 4, Descripcion = "Agua", CantidadAlmacen = 15, Activo = false},
            new Producto (){IdProducto = 5, Descripcion = "Jugo natural", CantidadAlmacen = 10, Activo = true},
        };

        public List<Producto> ListaProductos()
        {
            return _ListaProductos.Where(c => c.Activo).ToList();
        }

        public Producto ObtenerProducto(int id)
        {
            var cliente = _ListaProductos.Where(c => c.IdProducto == id);

            return cliente.FirstOrDefault();
        }

        public string DescripcionProducto(int Id)
        {
            Producto producto = ObtenerProducto(Id);
            return producto.Descripcion ?? "";
        }

        public bool Agregar(Producto nuevoProducto)
        {

            if (_ListaProductos.Any(c => c.Descripcion.ToUpper() == nuevoProducto.Descripcion.ToUpper()))
                return false;


            int IdNuevo = _ListaProductos.Max(x => x.IdProducto == 0 ? 0 : x.IdProducto);
            nuevoProducto.IdProducto = IdNuevo + 1;
            nuevoProducto.Activo = true;
            _ListaProductos.Add(nuevoProducto);

            return true;
        }
        public void ActualizarAlmacen(int IdProducto, int Cantidad)
        {
            _ListaProductos.ForEach(item =>
            {
                if(IdProducto == item.IdProducto)
                {
                    item.CantidadAlmacen = item.CantidadAlmacen - Cantidad;

                }
            });
        }
    }
}
