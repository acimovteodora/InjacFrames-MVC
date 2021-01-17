//using DatabaseBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCApp.Logic;
using MVCApp.Logic.Interfaces;
using MVCApp.DatabaseBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp
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
            services.AddControllersWithViews();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.Add(new ServiceDescriptor(typeof(Broker), new Broker(connectionString)));
            services.AddTransient<ILajsnaLogic, LajsnaLogic>();
            services.AddTransient<ITipLajsneLogic, TipLajsneLogic>();
            services.AddTransient<IAdresaLogic, AdresaLogic>();
            services.AddTransient<IGradLogic, GradLogic>();
            services.AddTransient<INalogLogic, NalogLogic>();
            services.AddTransient<IPorudzbinaLogic, PorudzbinaLogic>();
            services.AddTransient<IProformaLogic, ProformaLogic>();
            services.AddTransient<IStavkaProformeLogic, StavkaProformeLogic>();
            services.AddTransient<ICenaLogic, CenaLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
