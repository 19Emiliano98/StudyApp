using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudyApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [Authorize]
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
            try
            {
                var user = _dbcontext.Users.Where(x => x.Nombre == param.Nombre && x.Pass == param.Pass).FirstOrDefault();

                if (user == null)
                {
                    return BadRequest("Credenciales incorrectas");
                }

                string token = Jwt.GenerarToken(user);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Token: " + token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
