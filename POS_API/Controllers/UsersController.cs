using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS_API.Helpers;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly IConfiguration _config;
        public UsersController(AppDbContext appDb,IConfiguration config)
        {
            this.appDb = appDb;
            _config = config;
        }
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var userdetails = appDb.users.AsQueryable();
            return Ok(userdetails);
        }
      
        [HttpPost()]
        public IActionResult SignUp([FromBody] Users userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
               userObj.Password = Enc_Dec_Password.EncryptPassword(userObj.Password);
                appDb.users.Add(userObj);
                appDb.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Sign up Successfully"

                });
            }
        }
        [HttpPut()]
        public IActionResult UpdateUser([FromBody] Users userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            var user = appDb.users.AsNoTracking().FirstOrDefault(x => x.Id == userObj.Id);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                user.FullName = userObj.FullName;
                user.UserName = userObj.UserName;
                user.Password = userObj.Password;
                user.Mobile = userObj.Mobile;
                user.UserType = userObj.UserType;
                // appDb.Entry(userObj).State = EntityState.Modified;
                appDb.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User Updated Successfully"
                });
            }
        }
        [HttpDelete(("{id:int}"))]
        public IActionResult DeleteUser(int id)
        {
            var user = appDb.users.Find(id);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "user not Found"
                });
            }
            else
            {
                appDb.Remove(user);
                appDb.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User Id Deleted"
                });
            }
        }
        [HttpPost()]
        public IActionResult Login([FromBody] Users userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = appDb.users.Where(a => a.UserName == userObj.UserName).FirstOrDefault();
              if (user != null && Enc_Dec_Password.DecryptPassword(user.Password) == userObj.Password)
              //if(user !=null && user.Password==userObj.Password)
                {
                    var token = GenerateToken(userObj.UserName);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Logged In Successfully",
                        FullName=user.FullName,
                        UserType = user.UserType,
                        Jwttoken=token
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "User Not Found"
                    });
                }
            }
        }
        //Forgot password
        [HttpPost()]
        public IActionResult ForgotPassword([FromBody] Users userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var userforgot = appDb.users.Where(a => a.UserName == userObj.UserName && a.Mobile==userObj.Mobile).FirstOrDefault();
                 var ForgotPassword= Enc_Dec_Password.DecryptPassword(userforgot.Password);
                if(userforgot != null)
                {
                    var token = GenerateToken(userObj.UserName);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Password Get Successfully",
                        ForgotPassword,
                        Jwttoken = token
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "User Not Found"
                    });
                }
            }
        }
        private string GenerateToken(string username)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim(ClaimTypes.Email,username),
                new Claim("companyname","ijaz")
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return tokenhandler.WriteToken(token);
        }
    }


}
