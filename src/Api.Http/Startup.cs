namespace Api.Http
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
                    "WarriorPrinceApi",
                    new OpenApiInfo
                    {
                        Title = "Warrior Prince Api",
                        Version = "1.0.0",
                        Description = $"```Warrior Prince``` is an RPG based Character Design." +
                                      $"{Environment.NewLine}{Environment.NewLine}" +
                                      $"An Elf or Ogre warrior can be created or killed" +
                                      $"{Environment.NewLine}{Environment.NewLine}" +
                                      $"> Warriors can perform other activities like, Eat / Sleep / Fight",
                        Contact = new OpenApiContact
                        {
                            Name = "Designed by SpicyCoder",
                            Url = new Uri("https://github.com/spicycoder")
                        }
                    });

                Directory.GetFiles(
                        AppContext.BaseDirectory,
                        "*.xml",
                        SearchOption.TopDirectoryOnly)
                    .ToList()
                    .ForEach(x =>
                    {
                        swagger.IncludeXmlComments(x);
                    });

                swagger.DescribeAllEnumsAsStrings();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/></param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/WarriorPrinceApi/swagger.json", "Warrior Prince API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
