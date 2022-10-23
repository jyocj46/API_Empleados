using Microsoft.AspNetCore.Mvc;
using System.Linq;
using web_api.Models;
using System.Threading.Tasks;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadosController : Controller
    {
        private Conexion dbConexion;
        public EmpleadosController()
        {
            dbConexion = Conectar.Create();
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(dbConexion.Empleados.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var empleados = dbConexion.Empleados.SingleOrDefault(a => a.idEmpleados == id);
            if (empleados != null)
            {
                return Ok(empleados);
            }
            else
            {
                return NotFound(empleados);
            }
        }
        //Post se usa para enviar datos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados)
            {
                if (ModelState.IsValid)
                {
                    dbConexion.Empleados.Add(empleados);
                   await dbConexion.SaveChangesAsync();
                    return Ok(empleados);
                }
                else
                {
                    return NotFound();
                }
            }
        //Put cambia datos
        public async Task<ActionResult> Put([FromBody] Empleados empleados)
        {
            var v_empleados = dbConexion.Empleados.SingleOrDefault(a => a.idEmpleados == empleados.idEmpleados);
            if (v_empleados != null && ModelState.IsValid)
            {
                dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        //Elimina Datos
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empleados = dbConexion.Empleados.SingleOrDefault(a => a.idEmpleados == id);
            if (empleados != null)
            {
                dbConexion.Empleados.Remove(empleados);
               await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }

}