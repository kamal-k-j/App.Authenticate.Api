using App.Authenticate.Api.Data;
using App.Authenticate.Api.IoC;
using App.Authenticate.Api.Options;
using App.Authenticate.Api.Services;
using App.Authenticate.Api.Services.Authenticate;
using App.Authenticate.Api.Services.Register;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace App.Authenticate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                    Version = "v1"
                });
            });

            services.AddHealthChecks();

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.Configure<SecurityConfig>(Configuration.GetSection("SecurityConfig"));

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JwtConfig:SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IAuthenticateService, AuthenticateService>();
            services.AddTransient<ICreateUserToken, CreateUserToken>();
            services.AddTransient<IGetUser, GetUser>();
            services.AddTransient<IStoreUser, StoreUser>();
            services.AddTransient<IPasswordManager, PasswordManager>();
            services.AddTransient<IMapUserRegister, MapUserRegister>();
            services.AddTransient<IRegisterService, RegisterService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(Log.Logger).As<ILogger>();
            builder.RegisterModule(new CustomDatabaseRegistrationModule(Configuration, typeof(Startup).Assembly));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                });
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health", new HealthCheckOptions());
            });
        }
    }
}
