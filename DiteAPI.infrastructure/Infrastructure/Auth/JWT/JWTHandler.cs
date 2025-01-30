using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiteAPI.infrastructure.Infrastructure.Auth.JWT
{
    public interface IJwtHandler
    {
        LoginResponse Create(JwtRequest request);
    }

    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtSettings _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;

        public JwtHandler(IOptions<JwtSettings> options, IConfiguration configuration)
        {
            _options = options.Value;
            _configuration = configuration;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _configuration = configuration;
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!))
            };
            
        }

        public LoginResponse Create(JwtRequest request)
        {
            //var issuer = _configuration["JwtSettings:Issuer"];
            //var audience = _configuration["JwtSettings:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!);
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddDays(7);
            var claims = new List<Claim>
            {
                new Claim("sub", request.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowUtc.ToString(), ClaimValueTypes.Integer64),
                new Claim("unique_name", request.EmailAddress)
                //new Claim(JwtRegisteredClaimNames.Iss, issuer!),
                //new Claim(JwtRegisteredClaimNames.Aud, issuer!),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires, 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                //Issuer = issuer,
                //Audience = audience,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);


            return new LoginResponse
            {
                UserId = request.UserId,
                Username = request.Username,  
                Email = request.EmailAddress,
                Token = jwtToken,
                Expires = expires.ToString(),
                //IsEmailVerified = request.IsEmailVerified,
            };
        }
    }
}
