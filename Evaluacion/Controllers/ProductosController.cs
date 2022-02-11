using Evaluacion.Modelo;
using Evaluacion.Negocio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            NegProductos negProductos = new NegProductos();

            var content = negProductos.ListaProductos();

            return Content(JsonConvert.SerializeObject(content), "application/json");
        }

        [HttpPost("Agregar")]
        public IActionResult AgregarOrden(Producto NuevoProducto)
        {
            NegProductos negProductos = new NegProductos();

            var resultado = negProductos.Agregar(NuevoProducto);

            if (!resultado)
            {
                var nf = NotFound("El producto '"+NuevoProducto.Descripcion+"' ya existe.");
                return nf;
            }
            return Ok("Producto agregado");
        }

    }
}
