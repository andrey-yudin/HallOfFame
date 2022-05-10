namespace HallOfFame
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using HallOfFame.DataBase.DataAccess;
    using HallOfFame.DataBase.Repositories;
    using HallOfFame.Services;

    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get;}

        #endregion

        #region Constructror

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Constructor

        #region Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<IEmployeeService, EmployeeService>();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HallOfFame API v1"));

            InitializeDatabase(app);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<EmployeeContext>().Database.Migrate();
        }

        #endregion Methods
    }
}
