using BackPuppy.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackPuppy.Validaciones;

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

            services.AddDbContext<AplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PuppyPlanet")));
            services.AddEndpointsApiExplorer();

            services.AddHttpClient();

            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llaveJWT"])),
                ClockSkew = TimeSpan.Zero
            });
           // services.AddIdentity<ApplicationUser, IdentityRole>().AddErrorDescriber<SpanishIdentityErrorDescriber>();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));    
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
