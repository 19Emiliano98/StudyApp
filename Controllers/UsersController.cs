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
        public IActionResult Login([FromBody] User objeto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("Subject")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, DateTime.UtcNow.ToString()),
                new Claim("usuaio", objeto.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("APPKEY")));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    Environment.GetEnvironmentVariable("Issuer"),
                    Environment.GetEnvironmentVariable("Audience"),
                    claims,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: singIn
                );

            try
            {
                User usuario = _dbcontext.Users.Where(x => x.Nombre == objeto.Nombre && x.Pass == objeto.Pass).FirstOrDefault();

                if (usuario == null)
                {
                    return BadRequest("Credenciales incorrectas");
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}
