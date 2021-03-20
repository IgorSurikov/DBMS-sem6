using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Repositories;
using FootballStatisticsArchive.Services.Interfaces;
using FootballStatisticsArchive.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FootballStatisticsArchive.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/login");
                });

            services.AddTransient<IAccountReposetory, AccountReposetory>();
            services.AddTransient<IBaseReposetory, BaseReposetory>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITournamentRepository, TournamentRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ITournamentService, TournamentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
