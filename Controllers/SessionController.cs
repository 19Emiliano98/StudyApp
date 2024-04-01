using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyApp.Models;
using System;

namespace StudyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly DBAPIContext _dbcontext;
        public UsersController(DBAPIContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("VerUsuarios")]
        public IActionResult VerUsuarios()
        {
            List<User> users = new();

            try
            {
                users = _dbcontext.Users.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = users });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = users });
            }
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User param)
        {
            var user = _dbcontext.Users.Where(x => x.Nombre == param.Nombre && x.Pass == param.Pass).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("Credenciales incorrectas");
            }
            
            try
            {
                string token = Jwt.GenerarToken(user);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User param)
        {
            var user = _dbcontext.Users.Where(x => x.Email == param.Email).FirstOrDefault();

            if (user == null)
            {
                try
                {
                    _dbcontext.Users.Add(param);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ya existe uno, ingresa con el que ya creaste" });
        }
    }
}
