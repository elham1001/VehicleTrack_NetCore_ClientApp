using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleTrack_NetCore_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.OpenApi.Models;

namespace VehicleTrack_NetCore_API
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;

            _configurationRoot = new ConfigurationBuilder()
                               .SetBasePath(hostingEnvironment.ContentRootPath)
                               .AddJsonFile("appsettings.json")
                               .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                                               options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Vehicle API",
                    Version = "v1",
                    Description = "Vehicle Track API",
                    Contact = new OpenApiContact
                    {
                        Name = "Information Technology",
                        Email = "it@test.com",
                        Url = new Uri("https://domainName.com/"),
                    }
                });
                c.CustomSchemaIds(i => i.FullName);
                c.DocInclusionPredicate((docName, description) => true);

            });

            //services.AddSingleton<TimedHostedService, AppDbContext>();
            
            services.AddHostedService<TimedHostedService>();

            services.AddControllers();
            services.AddMvc();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:44383");
            }));
            services.AddSignalR();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DbInitializer.Seed(app);

            //signalR
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes => routes.MapHub<ChatHub>("/chathub"));
            app.UseHttpsRedirection();
           // app.UseMvc();

        }
    }
}
