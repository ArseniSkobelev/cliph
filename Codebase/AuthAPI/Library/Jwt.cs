using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cliph.Models;
using Microsoft.IdentityModel.Tokens;

namespace cliph.Library;

public static class Jwt
{
    public static string CreateJwt(string secret, string issuer, string audience, double expirationMinutes, IEnumerable<Claim> claims)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static Claim RetrieveClaimByClaimType(string token, ClaimType claimType)
    {
        var handler = new JwtSecurityTokenHandler();
        var parsedToken = handler.ReadJwtToken(token);

        Claim? foundClaim;

        try
        {
            foundClaim = parsedToken.Claims.FirstOrDefault(claim => claim.Type == claimType.ToString());
        }
        catch (Exception)
        {
            throw new SecurityTokenDecryptionFailedException("Unable to retrieve a claim with the given claim type");
        }
        
        if(foundClaim == null)
            throw new SecurityTokenDecryptionFailedException("Unable to retrieve a claim with the given claim type");

        return foundClaim;
    }
}