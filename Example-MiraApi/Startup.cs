using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiraThree
{
    using CoHAApi;
    using CoHAMVC;
    using CoHAPersistence;
    using Microsoft.EntityFrameworkCore;
    using Rooms;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opts => opts.Filters.Add<IoFilter>());
            services.AddTransient<IService<Student>, StudentService>();
            services.AddTransient<IRepository<Student>, EntityRepository<Student>>();
            services.AddTransient<IRepository<Room>, EntityRepository<Room>>();
            services.AddTransient<IService<Room>, CoHAService<Room>>();
            services.AddSingleton<DbContext>(sp => new DbContextInstance(@"C:\Databases\MiraDb3.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetService<DbContext>().Database.EnsureCreated();
        }
    }
}