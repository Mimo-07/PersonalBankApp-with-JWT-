using EmployeeAPI.DataLayer;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace EmployeeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly EmpDbContext _context;
        private readonly JWTSetting _setting;
        public JWTController(EmpDbContext context,IOptions<JWTSetting> setting)
        {
            _context = context;
            _setting = setting.Value;
        }

        [Route("Authenticate")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credentials usercred)
        {
            Credentials user = _context.tblCredentials.FirstOrDefault<Credentials>(c => (c.username == usercred.username && c.password == usercred.password && c.role==usercred.role));
            if(user==null)
            {
                return Unauthorized("Invalid Credentials");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_setting.securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.username),
                        new Claim(ClaimTypes.Role, user.role)
                    }),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var finalToken = tokenHandler.WriteToken(token);
            return Ok(finalToken);
        }
    }
}
