using Evaluacion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Negocio
{
    public class NegOrdenes
    {
        public static List<Orden> _ListaOrdenes = new List<Orden>
        {
            new Orden (){IdOrden = 1, Folio = 1, IdCatEstatus = 1},
            new Orden (){IdOrden = 2, Folio = 2, IdCatEstatus = 1},
            new Orden (){IdOrden = 3, Folio = 3, IdCatEstatus = 1},
            new Orden (){IdOrden = 4, Folio = 4, IdCatEstatus = 2},
        };

        public IEnumerable<Orden> ObtenerOrdenes()
        {
            return _ListaOrdenes;
        }

        public IEnumerable<dynamic> ObtenerOrdenesEstatus(int IdCatEstatus)
        {
            NegEstatus negEstatus = new NegEstatus();
            NegProductosOrdenes productosOrdenes = new NegProductosOrdenes();

            var folios = _ListaOrdenes.Where(c => c.IdCatEstatus == IdCatEstatus).ToList();

            List<dynamic> Resultado = new List<dynamic>();

            folios.ForEach(item => 
            {
             //   List<Producto> productosOrden = productosOrdenes.ObtenerProductosOrden(item.IdOrden);

                Resultado.Add(
                    new {
                        Folio = "#"+item.Folio.ToString("D6"),
                        item.IdOrden,
                        item.IdCatEstatus,
                        CatEstatus = negEstatus.DescripcionEstatus(item.IdCatEstatus),
                        Productos = productosOrdenes.ObtenerProductosOrden(item.IdOrden)
                    });


            });

            return Resultado;
        }

        public dynamic ObtenerOrden(string Folio)
        {
            NegProductos negProductos = new NegProductos();
            NegEstatus negEstatus = new NegEstatus();
            NegProductosOrdenes negProductosOrdenes = new NegProductosOrdenes();


            int auxFolio = Convert.ToInt32(Folio.Replace("#",""));
            Orden orden = _ListaOrdenes.Where(c => c.Folio == auxFolio).FirstOrDefault();

            if (orden == null)
                return null;


            var result = new
            {
                Folio = "#" + orden.Folio.ToString("D6"),
                orden.IdOrden,
                orden.IdCatEstatus,
                CatEstatus = negEstatus.DescripcionEstatus(orden.IdCatEstatus),
                Productos = negProductosOrdenes.ObtenerProductosOrden(orden.IdOrden)
            };

            return result;
        }

        public Orden CrearOrden()
        {
            Orden nuevaOrden = new Orden();

            int NuevoFolio = _ListaOrdenes.Max(x => x == null ? 0 : x.Folio);
            int NuevoId = _ListaOrdenes.Max(x => x == null ? 0 : x.IdOrden);

            nuevaOrden.IdOrden = NuevoId + 1;
            nuevaOrden.Folio = NuevoFolio + 1;
            nuevaOrden.IdCatEstatus = 1;

            _ListaOrdenes.Add(nuevaOrden);

            return nuevaOrden;
        }

        public bool ModificarOrden(int IdOrden, int IdCatEstatus)
        {

            foreach (Orden item in _ListaOrdenes)
            {
                if(item.IdOrden == IdOrden)
                {
                    item.IdCatEstatus = IdCatEstatus;
                    return  true;
                }
            }

            return false;
        }

        public int ModificarOrden(int Folio, int IdProducto, int Cantidad)
        {
            try
            {
                Orden orden = _ListaOrdenes.Where(c => c.Folio == Folio).FirstOrDefault();
                if (orden == null)
                    return 2;

                ProductoOrden NuproductoOrden = new ProductoOrden
                {
                    IdOrden = orden.IdOrden,
                    IdProducto = IdProducto,
                    Cantidad = Cantidad
                };

                NegProductosOrdenes negProductosOrdenes = new NegProductosOrdenes();
                negProductosOrdenes.AgregarProductoOrden(NuproductoOrden);


                return 1;
            }
            catch (Exception)
            {
                return 0;
               // throw;
            }
        }

    }
}
