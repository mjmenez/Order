using Evaluacion.Modelo;
using Evaluacion.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Net.Mime;
using Newtonsoft.Json;
//using System.Web.Script.Serialization;
namespace Evaluacion.Controllers
{
   // [Produces("aplication/Json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            NegOrdenes negOrdenes = new NegOrdenes();

            var content = negOrdenes.ObtenerOrdenesEstatus(1);


            return Content(JsonConvert.SerializeObject(content), "application/json");
        }

        [HttpGet("{folio}")]
        public IActionResult Get(string Folio)
        {
            NegOrdenes negOrdenes = new NegOrdenes();

            var ResultOrden = negOrdenes.ObtenerOrden(Folio);

            if (ResultOrden == null)
            {
                var nf = NotFound("La orden con folio  " + Folio + " no existe.");
                return nf;
            }

            return Ok(ResultOrden);
        }

        [HttpPost("Agregar")]
        public IActionResult AgregarOrden(List<ProductoOrden> NuevaOrden)
        {
            NegProductosOrdenes rpCli = new NegProductosOrdenes();
            var resultado = rpCli.AgregarProductoOrden(NuevaOrden);
            return Content(JsonConvert.SerializeObject(resultado), "application/json");
        }

        [HttpPost("Modificar")]
        public IActionResult CambiarEstatus(int IdOrden, int IdCatEstatus)
        {
            NegOrdenes rpCli = new NegOrdenes();
            var Resultado = rpCli.ModificarOrden(IdOrden, IdCatEstatus);
            return Ok(Resultado);
        }

        [HttpPost("AgregarProducto")]
        public IActionResult AgregarProducto(string Folio, int IdProducto, int Cantidad)
        {
            NegOrdenes rpCli = new NegOrdenes();

            int auxFolio = Convert.ToInt32(Folio.Replace("#", ""));

            int Resultado = rpCli.ModificarOrden(auxFolio, IdProducto, Cantidad);


            if (Resultado == 2)
            {
                var nf = NotFound("La orden con folio" + Folio + " no existe.");
                return nf;
            }

            if (Resultado == 0)
            {
                var nf = NotFound("Error al agregar producto a la orden");
                return nf;
            }


            return Ok("Modificacion existosa");
        }

        [HttpPost("Consultar")]
        public IActionResult ConsultarOrdenes(int IdCatEstatus)
        {
            NegOrdenes negOrdenes = new NegOrdenes();

            var content = negOrdenes.ObtenerOrdenesEstatus(IdCatEstatus);


            return Content(JsonConvert.SerializeObject(content), "application/json");
        }

    }
}
