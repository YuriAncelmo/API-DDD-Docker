using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.OpenApi.Models;
using API_FeirasLivresSP.Exceptions.Filter;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

namespace API_FeirasLivresSP
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
            services.AddControllers(options =>
            {
                //Filter Exceptions 
                options.Filters.Add<HttpResponseExceptionFilter>();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Local onde será gravado os logs 
            loggerFactory.AddFile("Logs/API-{Date}.txt");

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
    }
}
