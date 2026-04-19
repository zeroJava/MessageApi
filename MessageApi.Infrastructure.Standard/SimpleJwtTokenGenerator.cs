using MessageApi.Application;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MessageApi.Infrastructure;

public class SimpleJwtTokenGenerator : ITokenGenerator
{
   public string GenerateToken(string userId, string role)
   {
      // Replace with proper security
      const string jwtKey = "helloworld123445789qwertyuiop123456789asdfghjkl123456789zxcvbnm123456789qazwsxedcrfvtgbyhnujmikolp123456789";
      const string issuer = "Message.Api";
      const string audience = "Testusers";
      Claim[] claims =
      [
         new Claim(ClaimTypes.NameIdentifier, userId),
         new Claim(ClaimTypes.Role, role)
      ];
      DateTime expireDate = DateTime.UtcNow.AddHours(1);
      SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtKey));
      SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
      JwtSecurityToken securityToken = new(issuer, audience, claims, null, expireDate, credentials);
      return new JwtSecurityTokenHandler().WriteToken(securityToken);
   }
}