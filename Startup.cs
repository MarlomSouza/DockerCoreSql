using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docker_app_compose.Data.Context;
using docker_app_compose.Data.Repository;
using docker_app_compose.Dominio;
using docker_app_compose.Dominio.Repository;
using docker_app_compose.Infra;
using DockerCoreSql.Dominio;
using DockerCoreSql.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace docker_app_compose
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MyConfiguration>(Configuration.GetSection("Endereco_Fila"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<Pessoa>), typeof(PessoaRepository));
            services.AddScoped(typeof(PessoaRepository), typeof(PessoaRepository));
            services.AddScoped(typeof(IFila<Pessoa>), typeof(Fila));
            services.AddScoped(typeof(Consumidor), typeof(Consumidor));
            

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseMvc();
        }
    }
}
