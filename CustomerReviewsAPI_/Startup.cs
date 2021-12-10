
using CustomerReviewsAPI_.Models;
using CustomerReviewsAPI_.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CustomerReviewsAPI_
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


            #region DBContext as a service
            IServiceCollection serviceCollection = services.AddDbContext<ApplicationDBContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("ReviewDBConnection"));
            }
            );
            #endregion

            #region ProductService,CampaignService,CompanyService,OnBeforeSave service

            services.AddTransient<ProductService>();

            services.AddTransient<CampaignService>();

            services.AddTransient<CompanyService>();

            services.AddScoped<OnBeforeSave>();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerReviewsAPI_", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerReviewsAPI_ v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
