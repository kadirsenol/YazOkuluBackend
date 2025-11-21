using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using YazOkulu.Data.Models;

namespace YazOkulu.GENAppService.Helper
{
    public static class JwtHelper
    {
        public static string GenerateToken(User user, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.MobilePhone, user.Gsm),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim("Faculty", user.FacultyTypeID.ToString() ?? string.Empty),
                new Claim("Department", user.DepartmentTypeID.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Role, user.RoleTypeID.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
