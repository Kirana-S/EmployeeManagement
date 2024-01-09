using EGWebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EGWebApplication3
{

    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
      public  void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => 
                options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<IdentityUser,IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            
            services.AddMvc(options => options.EnableEndpointRouting = false);
           
            services.AddMvc().AddXmlSerializerFormatters();

           services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
       


        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseStatusCodePages();
                 app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
       
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
