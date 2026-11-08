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
    [EnableCors(PolicyName = "AllowOnlyGoogle")]
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
            byte [] key = null;
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and Passwoes");
            }
            LoginResponceDTO responce = new() { Username = model.Username };
            if (model.Policy.Equals("Google"))
            {
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKeyForGoogle"));
            }
            else
            {
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKeyForMicrosoft"));
            }



            if (model.Username == "Hassan" && model.Password == "Hsn123")
            {
                


               




                    var TokenHandler = new JwtSecurityTokenHandler();
                var tokendescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        // Username
                        new Claim(ClaimTypes.Name, model.Username),
                        // Role
                        new Claim(ClaimTypes.Role, "Admin")


                    }),
                    Expires = DateTime.UtcNow.AddHours(4),
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
