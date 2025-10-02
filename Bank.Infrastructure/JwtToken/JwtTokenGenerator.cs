
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Infrastructure.JwtToken;

public class JwtTokenGenerator : IJwtTokenGenerator
{

    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateTokenAsync(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        var email = customer.Email ?? throw new Exception("Customer email cannot be null");
        var firstName = customer.FirstName ?? "";
        var lastName = customer.LastName ?? "";

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim(ClaimTypes.Role, "Admin")
        };

        

        var keyString = _configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(keyString))
            throw new Exception("JWT Key is missing in configuration");

        var issuer = _configuration["Jwt:Issuer"] ?? throw new Exception("JWT Issuer is missing");
        var audience = _configuration["Jwt:Audience"] ?? throw new Exception("JWT Audience is missing");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}