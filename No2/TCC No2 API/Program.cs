
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TCC_No2_API.Repositories;
using TCC_No2_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TCC_No2_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("super_secret_key_123")
                        ),

                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddDbContext<Data.AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserAuthenService, UserAuthenService>();
            builder.Services.AddScoped<IUserAuthenRepository, UserAuthenRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy => policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            var app = builder.Build();

            app.UseCors("AllowAngular");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
