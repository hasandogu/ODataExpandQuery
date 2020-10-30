// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using ODataExpandQuery.Models;
using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace ODataExpandQuery
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
            services.AddDbContext<CategoryContext>(opt => opt.UseLazyLoadingProxies().UseInMemoryDatabase("CategoryContext"));

            services.AddRouting();

            services.AddOData();

            // Auto Mapper Configuration
            // Doing this manually because we need to inject storage provider into automapper
            services.AddSingleton(provider => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());


//            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            IEdmModel model = EdmModelBuilder.GetEdmModel();

/*            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                builder.MapODataServiceRoute("odata", "odata", model);
            });
            */
            app.UseEndpoints(endpoint =>
            {
                endpoint.EnableDependencyInjection();

                // Configure OData default query settings
                endpoint.Expand().Select().Filter().OrderBy().Count().MaxTop(20).SkipToken();
                endpoint.MapODataRoute("odata", "odata", model);
            });

        }
    }
}
