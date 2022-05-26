using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DDDWebAPI.Infrastructure.Data;
using Autofac;
using DDDWebAPI.Infrastruture.CrossCutting.IOC;
using System.Reflection;

namespace DDDWebAPI.Presentation
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

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>// Configura comportamento da API quando tem problemas relacionados ao modelo 
            {
                options.InvalidModelStateResponseFactory = context =>
                    new BadRequestObjectResult(context.ModelState)
                    {
                        ContentTypes =
                        {
                            System.Net.Mime.MediaTypeNames.Application.Json
                        }
                    };
                options.ClientErrorMapping[StatusCodes.Status404NotFound].Title = "Não encontrado";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Where the logs is located 
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
            catch (Exception) { throw; }
        }
        public void ConfigureContainer(ContainerBuilder Builder)
        {
            Builder.RegisterModule(new ModuleIOC());
        }
    }
}
