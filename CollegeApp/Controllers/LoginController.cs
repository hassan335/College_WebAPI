using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "AllowOnyCoogle")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and Passwoes");
            }
            LoginResponceDTO responce = new() { Username = model.Username };
            if (model.Username == "Hassan" && model.Password == "Hsn123")
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));
                var TokenHandler = new JwtSecurityTokenHandler();
                var tokendescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        // Username
                        new Claim(ClaimTypes.Name, model.Username),
                        // Role
                        new Claim(ClaimTypes.Name, "Admin")


                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)



                };

                var token = TokenHandler.CreateToken(tokendescriptor);
                responce.token = TokenHandler.WriteToken(token);
                


            }
            else
            {
                return BadRequest("Invalid user and pass");
            }

            return Ok(responce);
        }







    }
}
