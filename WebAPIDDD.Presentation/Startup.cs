using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DDDWebAPI.Infrastructure.Data;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Application.Services;
using DDDWebAPI.Domain.Services.Services;
using DDDWebAPI.Domain.Core.Interfaces.Services;
using DDDWebAPI.Infrastruture.Repository.Repositorys;
using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Map;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Interfaces;
using Autofac;
using DDDWebAPI.Infrastruture.CrossCutting.IOC;

namespace WebAPIDDD.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddControllers(options =>
                {
                //Filter Exceptions 
                //options.Filters.Add<HttpResponseExceptionFilter>();//TODO: Colocar para mapear as exceptions

                 })
                .ConfigureApiBehaviorOptions(options =>// Configura comportamento da API quando tem problemas relacionados ao modelo 
                {
                    options.InvalidModelStateResponseFactory = context =>
                        new BadRequestObjectResult(context.ModelState)
                        {
                            ContentTypes =
                            {
                            Application.Json,
                            }
                        };
                    options.ClientErrorMapping[StatusCodes.Status404NotFound].Title = "Não encontrado";
                });

                var host = Configuration["DBHOST"] ?? "localhost";
                var port = Configuration["DBPORT"] ?? "3306";
                var password = Configuration["MYSQL_PASSWORD"] ?? Configuration.GetConnectionString("MYSQL_PASSWORD");
                var userid = Configuration["MYSQL_USER"] ?? Configuration.GetConnectionString("MYSQL_USER");
                var usersDataBase = Configuration["MYSQL_DATABASE"] ?? Configuration.GetConnectionString("MYSQL_DATABASE");

                string connString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";

                services.AddDbContext<MySqlContext>(optionsBuilder =>
                {
                    optionsBuilder.UseMySQL(connString);
                });

                //services.AddMvc();
                //services.AddMemoryCache();

                //services.AddScoped<IApplicationServiceFeira, ApplicationServiceFeira>();
                //services.AddScoped<IServiceFeira, ServiceFeira>();
                //services.AddScoped<IRepositoryFeira, RepositoryFeira>();
                //services.AddScoped<IMapperFeira, MapperFeira>();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Desafio de construção de API - Unico",
                        Description = "Construir uma API que trabalhe os dados de feiras livres em um banco de dados",
                        Contact = new OpenApiContact
                        {
                            Name = "Yuri Gabriel Correa Ancelmo",
                            Url = new Uri("https://www.linkedin.com/in/yuri-ancelmo/")
                        }
                    });
                });
            }catch { throw; }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Local onde será gravado os logs 
            loggerFactory.AddFile("Logs/API-{Date}.txt");
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseExceptionHandler(exceptionHandlerApp =>
                    {
                        exceptionHandlerApp.Run(async context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        // using static System.Net.Mime.MediaTypeNames;
                        context.Response.ContentType = Text.Plain;

                            await context.Response.WriteAsync("Ocorreu um erro inesperado.");

                            var exceptionHandlerPathFeature =
                                context.Features.Get<IExceptionHandlerPathFeature>();


                            if (exceptionHandlerPathFeature?.Error is Exception exe)
                            {
                                await context.Response.WriteAsync(exe.Message);
                            }
                            if (exceptionHandlerPathFeature?.Path == "/")
                            {
                                await context.Response.WriteAsync(" Page: Home.");
                            }
                        });
                    });

                }
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = String.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception) { throw ; }
        }
        public void ConfigureContainer(ContainerBuilder Builder) 
        {
            Builder.RegisterModule(new ModuleIOC());
        }
    }
}
