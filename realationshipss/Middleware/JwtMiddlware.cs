using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace realationshipss.Middleware
{
    public class JwtMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddlware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        //skip validation in login and swagger ui (part 1)
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/api/Authentication/login")||
                context.Request.Path.StartsWithSegments("/api/Register/register"))
            {

                await _next(context);
                return;
            }

            //getting token (part 2)
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("token is missing");
                return;
            }
            //(part 3)
            var tokenhandler = new JsonWebTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwt:SecretKey"]!);

            try

            {
                //(part 4)  do we need all this things?
                var ValidationResult = await tokenhandler.ValidateTokenAsync(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                });

                //(part 5)
                if (!ValidationResult.IsValid)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("unauthorized: invalid token");
                    return;
                }

                context.User = new ClaimsPrincipal(ValidationResult.ClaimsIdentity);
            }
            catch (Exception ex)
            {
                //(part 6)
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Unauthorized: {ex.Message}");
                return;
            }
            await _next(context);

        }
    }
}
