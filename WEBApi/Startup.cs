using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WEBApi.Extensions;
using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.DB;
using WEBApi.DTOs;
using MediatR;

namespace WEBApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CheapWaterAPI", Version = "v1" });
            });
            
            services.AddDbContext<AuthContext>(x =>
                    x.UseNpgsql(Configuration.GetConnectionString("Standard"),
                    options => options.MigrationsAssembly(nameof(WEBApi)))
            );
            
            services.AddEncryption();

            services.AddRepositories();

            services.AddValidators();
           
            services.AddJWTokens(Configuration);

            services.AddMediatR(typeof(Startup));

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBApi v1"));
            }

            app.UseWebSocketsServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
