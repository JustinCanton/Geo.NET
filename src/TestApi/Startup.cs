// <copyright file="Startup.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi
{
    using Geo.ArcGIS.DependencyInjection;
    using Geo.Bing.DependencyInjection;
    using Geo.Google.DependencyInjection;
    using Geo.Here.DependencyInjection;
    using Geo.MapQuest.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            services.AddSwaggerDocument();

            services.AddGoogleServices(options => options.UseKey(string.Empty));
            services.AddBingServices(options => options.UseKey(string.Empty));
            services.AddArcGISServices(options => options.UseClientKeys(string.Empty, string.Empty));
            services.AddHereServices(options => options.UseKey(string.Empty));
            services.AddMapQuestServices(options => options.UseKey(string.Empty).UseLicensedEndpoints());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
