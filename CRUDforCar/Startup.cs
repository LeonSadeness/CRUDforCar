using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using CRUDforCar.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CRUDforCar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ���� ����� ���������� �� ����� ����������. ����������� ���� ����� ��� ���������� �������� � ���������.
        public void ConfigureServices(IServiceCollection services)
        {
            string DB = Configuration["UseDB"].ToLower();

            if (DB.StartsWith("mongo"))
            {
                services.Configure<CarsDatabaseSettings>(Configuration.GetSection("MongoDbSettings"));
                services.AddSingleton<ICarsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CarsDatabaseSettings>>().Value);

                services.AddSingleton<ICarsDBService>(sp => new CarsMongoDBService(sp.GetService<ICarsDatabaseSettings>()));
            }
            else if (DB.StartsWith("postgre"))
            {
                services.Configure<CarsDatabaseSettings>(Configuration.GetSection("PostgresSettings"));
                services.AddSingleton<ICarsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CarsDatabaseSettings>>().Value);

                services.AddSingleton<ICarsDBService>(sp => new CarsPostgresService(sp.GetService<ICarsDatabaseSettings>()));
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.Yellow;
                System.Console.WriteLine("\"UseDB\" configuration not found, or set incorrectly." +
                    " Be sure to specify the desired database in \"appsettings.json\" with the value \"UseDB\"\n");
            }

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing()); ;
        }

        // ���� ����� ���������� �� ����� ����������. ����������� ���� ����� ��� ��������� ��������� HTTP-�������.
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
