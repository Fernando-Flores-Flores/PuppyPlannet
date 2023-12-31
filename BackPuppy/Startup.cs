﻿using BackPuppy.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackPuppy.Validaciones;
using BackEnd2023.Utilitarios;
using BackPuppy.Utilidades;
using BackPuppy.Dtos;
using BackPuppy.Facade;

namespace BackPuppy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddCors();

            services.AddDbContext<AplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PuppyPlanet")));
            services.AddEndpointsApiExplorer();

            services.AddHttpClient();


            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

            services.AddHttpContextAccessor();

            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false; // Sin requerir dígitos
                options.Password.RequireLowercase = false; // Sin requerir minúsculas
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false; // No requerir caracteres no alfanuméricos
                options.Password.RequiredLength = 1;
                // Otras configuraciones de contraseñas
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llaveJWT"])),
                ClockSkew = TimeSpan.Zero
            });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<EmailService>();
            // services.AddIdentity<ApplicationUser, IdentityRole>().AddErrorDescriber<SpanishIdentityErrorDescriber>();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));    
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200");
                options.AllowAnyMethod();
                options.AllowAnyHeader();

            });
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
