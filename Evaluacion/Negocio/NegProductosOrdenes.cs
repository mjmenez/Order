using Evaluacion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Negocio
{
    public class NegProductosOrdenes
    {
        public static List<ProductoOrden> _ListaProctosOrden = new List<ProductoOrden>
        {
            new ProductoOrden (){IdOrdenProducto = 1, IdProducto = 1, IdOrden = 1, Cantidad = 2 },
            new ProductoOrden (){IdOrdenProducto = 2, IdProducto = 3, IdOrden = 1, Cantidad = 1 },
            new ProductoOrden (){IdOrdenProducto = 3, IdProducto = 2, IdOrden = 2, Cantidad = 1 },
            new ProductoOrden (){IdOrdenProducto = 4, IdProducto = 2, IdOrden = 4, Cantidad = 3},
            new ProductoOrden (){IdOrdenProducto = 4, IdProducto = 4, IdOrden = 3, Cantidad = 3},
        };
        public List<dynamic> ObtenerProductosOrden(int IdOrden)
        {
            NegProductos negProductos = new NegProductos();

            return _ListaProctosOrden.Where(c => c.IdOrden == IdOrden)
                .Select(s => new {
                s.Cantidad,
                s.IdProducto,
                Producto = negProductos.DescripcionProducto(s.IdProducto)
                } ).ToList<dynamic>();
        }

        public bool AgregarProductoOrden(ProductoOrden nuevoProducto)
        {

            try
            {
                NegOrdenes negOrdenes = new NegOrdenes();
                NegProductos negProductos = new NegProductos();
                Orden NuevaOrden = negOrdenes.CrearOrden();

                    int NuevoId = _ListaProctosOrden.Max(x => x == null ? 0 : x.IdOrdenProducto);

                    nuevoProducto.IdOrdenProducto = NuevoId + 1;
                    _ListaProctosOrden.Add(nuevoProducto);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AgregarProductoOrden(List<ProductoOrden> nuevosProductos)
        {

            try
            {
                NegOrdenes negOrdenes = new NegOrdenes();
                NegProductos negProductos = new NegProductos();
                Orden NuevaOrden = negOrdenes.CrearOrden();

                foreach (ProductoOrden item in nuevosProductos)
                {
                    int NuevoId = _ListaProctosOrden.Max(x => x == null ? 0 : x.IdOrdenProducto);

                    item.IdOrden = NuevaOrden.IdOrden;
                    item.IdOrdenProducto = NuevoId + 1;
                    negProductos.ActualizarAlmacen(item.IdProducto, item.Cantidad);
                    _ListaProctosOrden.Add(item);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
