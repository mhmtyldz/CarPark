using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarPark.User.Localizer;
using CarPark.User.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarPark.User
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

            //Buraya Localization servisimizi ekleyece�iz. 
            //Tamam ekledik ancak size bahsetti�im dosyalar burada buluncak ve sende
            //Buradaki klas�rden al oku diycem. Onun i�in ayarlar� tan�mlamam laz�m
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc() // Suffix dememizin sebebi viewde ki yaz�l�m olay� ile ilgli
                              //E�er suffix dersek bu �ekilde kullanabiliyoruz. 
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(
                options => options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedModelResources).GetTypeInfo().Assembly.FullName);
                    return factory.Create(nameof(SharedModelResources), assemblyName.Name);
                }
                );

            //Hangi diller desteklenecek ise burada o dillerin tan�mlamas�n� yapaca��z.
            //Biz �uanl�k sadece ingilizce,T�rk�e ve Arap�a dillerinin desteklenmesini istiyoruz
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                //Desteklenen diller
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR"),
                    new CultureInfo("ar-SA")
                };
                //Default Olarak Kullan�lacak Yani hi� bir dil belirtilmedi�inde T�rk�e olsun diyoruz
                opt.DefaultRequestCulture = new RequestCulture("en-US");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
                //Burdaki ayarda da diyoruz ki Ben localizationu hem Querystring de 
                //Hem cookie de 
                //hemde headerda g�nderebilirim
                //opt.RequestCultureProviders = new List<IRequestCultureProvider>
                //{

                //    new QueryStringRequestCultureProvider(),
                //    new CookieRequestCultureProvider(),
                //    new AcceptLanguageHeaderRequestCultureProvider()
                //};
                //Bu ayar ile /en-US/contact/add /tr-TR/contact/add  yap�yoruz
                opt.RequestCultureProviders = new[]{new RouteDataRequestCultureProvider() { Options = opt }};


            });

            //services.AddSingleton<SharedLocalizer>();

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
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
