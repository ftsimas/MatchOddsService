using MatchOddsService.Factories;
using MatchOddsService.Models;
using MatchOddsService.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MatchOddsService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MatchOddsServiceDbContext>(options =>
            {
                var connectionStringKey = "ConnectionString";
                var connectionString = Configuration.GetConnectionString(connectionStringKey) ?? throw new ArgumentNullException(connectionStringKey);
                options.UseSqlServer(connectionString);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(kvp => kvp.Value.Errors.Count > 0)
                        .Select(ErrorDTOFactory.ModelBindingError)
                        .ToArray();

                    return new BadRequestObjectResult(ErrorDTOFactory.ErrorCollectionDTO(errors));
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Match Odds Service API",
                    Description = "This is a demo service for handling matches and match odds.",
                    Contact = new OpenApiContact
                    {
                        Name = "Filippos Tsimas",
                        Url = new Uri("https://github.com/ftsimas"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GNU AGPLv3",
                        Url = new Uri("https://www.gnu.org/licenses/agpl-3.0.en.html"),
                    }
                });

                options.CustomSchemaIds(type => type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? type.Name);

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Documentation.xml");
                options.IncludeXmlComments(xmlPath);

                options.SchemaFilter<EnumSchemaFilter>(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MatchOddsServiceDbContext>();
                context.Database.Migrate();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Match Odds Service API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
