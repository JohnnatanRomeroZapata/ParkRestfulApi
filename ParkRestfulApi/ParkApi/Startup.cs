using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkApi.Data;
using ParkApi.Repository;
using ParkApi.Repository.IRepositories;
using ParkApi.ParkMapper;
using System.Reflection;
using System.IO;
using System;

namespace ParkApi
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
            services.AddControllers();


            //---------- Added by me

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ParkApiConnectionString")));

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();

            services.AddAutoMapper(typeof(ParkMappings));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ParkOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Park API",
                    Version = "1",
                    Description = "Description Park API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Johnnatan",
                        Email = "romerozapatajonathan@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/johnnatan-romero-zapata-2b359455/")
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(); //Added by me

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/ParkOpenAPISpec/swagger.json", "Park API");
                options.RoutePrefix = "";
            }); //Added by me

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
