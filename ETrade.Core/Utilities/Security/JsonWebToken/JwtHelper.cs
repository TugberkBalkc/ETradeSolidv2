using ETrade.Core.Entities.Concrete;
using ETrade.Core.Extensions;
using ETrade.Core.Utilities.Security.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Core.Utilities.Security.JsonWebToken
{
    public class JwtHelper : ITokenHelper
    {
        internal IConfiguration _configuration { get; }
        
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateAccessToken(User user, IQueryable<OperationClaim> operationClaims)
        {
           
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpirationInMinutes);

            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken
            (TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, IQueryable<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken
                (
                  audience: tokenOptions.Audience,
                  issuer: tokenOptions.Issuer,
                  claims: SetClaims(user, operationClaims),
                  notBefore: DateTime.Now,
                  expires: _accessTokenExpiration,
                  signingCredentials: signingCredentials
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, IEnumerable<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(opc => opc.Name).ToArray());

            return claims;
        }
    }
}
