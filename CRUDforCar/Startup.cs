using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using CRUDforCar.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUDforCar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается во время выполнения. Используйте этот метод для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            string useDb = Configuration["UseDB"].ToLower();
            string stringConnect = Configuration.GetConnectionString(useDb);
            string nameDb = Configuration["NameDb"];
            string collection = Configuration["Collection"];

            if (useDb.StartsWith("mongo"))
            {
                var serviceDB = new CarsMongoDBService(stringConnect, nameDb, collection);
                services.AddSingleton<IRepositoryCar, CarsMongoDBService>(sp => serviceDB);
            }
            else if (useDb.StartsWith("postgre"))
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<CarsPostgresContext>(opt =>
                opt.UseNpgsql(stringConnect));
                services.AddScoped<IRepositoryCar, CarsPostgresService>();
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.Yellow;
                System.Console.WriteLine("\"UseDB\" configuration not found, or set incorrectly." +
                    " Be sure to specify the desired database in \"appsettings.json\" with the value \"UseDB\"\n");
            }

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing()); ;
        }

        // Этот метод вызывается во время выполнения. Используйте этот метод для настройки конвейера HTTP-запроса.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
