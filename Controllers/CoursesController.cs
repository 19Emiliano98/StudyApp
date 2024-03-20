using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Models;

namespace StudyApp.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public readonly DBAPIContext _dbcontext;

        public CoursesController(DBAPIContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Course> lista = new();

            try
            {
                lista = _dbcontext.Courses.Include(c => c.oCategoria).ToList();
                
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idCourse}")]
        public IActionResult Obtener(int idCourse)
        {
            Course? oCourse = _dbcontext.Courses.Find(idCourse);

            if (oCourse == null)
            {
                return BadRequest("Producto no Encontrado");
            }

            try
            {
                oCourse = _dbcontext.Courses.Include(c => c.oCategoria).Where(p => p.IdCourse == idCourse).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oCourse });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oCourse });
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Course objeto)
        {
            try
            {
                _dbcontext.Courses.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Course objeto)
        {
            Course? oCourse = _dbcontext.Courses.Find(objeto.IdCourse);

            if (oCourse == null)
            {
                return BadRequest("Producto no Encontrado");
            }

            try
            {
                oCourse.Nombre = objeto.Nombre is null ? oCourse.Nombre : objeto.Nombre;
                oCourse.Descripcion = objeto.Descripcion is null ? oCourse.Descripcion : objeto.Descripcion;
                oCourse.Duracion = objeto.Duracion is null ? oCourse.Duracion : objeto.Duracion;
                oCourse.IdCategoria = objeto.IdCategoria is null ? oCourse.IdCategoria : objeto.IdCategoria;
                oCourse.Precio = objeto.Precio is null ? oCourse.Precio : objeto.Precio;

                _dbcontext.Courses.Update(oCourse);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idCourse:int}")]
        public IActionResult Eliminar(int idCourse)
        {
            Course? oProducto = _dbcontext.Courses.Find(idCourse);

            if (oProducto == null)
            {
                return BadRequest("Producto no Encontrado");
            }

            try
            {
                _dbcontext.Courses.Remove(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
