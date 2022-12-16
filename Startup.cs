namespace Sportiada.Web
{
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sportiada.Data;
    using Services.AlpineSki.Interfaces;
    using Services.AlpineSki.Implementations;
    using Services.Football.Interfaces;
    using Services.Football.Implementations;
    using Services.Admin.Interfaces;
    using Services.Admin.Implementations;

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
            services.AddDbContext<SportiadaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SportiadaDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<ICompetitionAlpineSkiService, CompetitionAlpineSkiService>();
            services.AddTransient<ISkierAlpineSkiService, SkierAlpineSkiService>();
            services.AddTransient<IResultAlpineSkiService, ResultAlpineSkiService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ISeasonService, SeasonService>();
            services.AddTransient<IRoundService, RoundService>();
            services.AddTransient<ICountryAdminService, CountryAdminService>();
            services.AddTransient<IFootballPlayerAdminService, FootballPlayerAdminService>();
            services.AddTransient<ICityAdminService, CityAdminService>();
            services.AddTransient<IFootballTeamAdminService, FootballTeamAdminService>();
            services.AddTransient<ISeasonAdminService, SeasonAdminService>();
            services.AddTransient<IFootballTournamentService, FootballTournamentService>();
            services.AddTransient<IFootballCompetitionAdminService, FootballCompetitionAdminService>();
            services.AddTransient<ICompetitionTypeAdminService, CompetitionTypeAdminService>();
            services.AddTransient<IFootballRoundAdminService, FootballRoundAdminService>();
            services.AddTransient<IFootballCardTypeAdminService, FootballCardTypeAdminService>();
            services.AddTransient<IFootballGoalTypeAdminService, FootballGoalTypeAdminService>();
            services.AddTransient<IFootballSidelineReasonAdminService, FootballSidelineReasonAdminService>();
            services.AddTransient<IFootballRefereeTypeAdminService, FootballRefereeTypeAdminService>();
            services.AddTransient<IFootballRefereeAdminService, FootballRefereeAdminService>();
            services.AddTransient<IFootballPlayerTransferAdminService, FootballPlayerTransferAdminService>();
            services.AddTransient<IFootballPlayerCountryAdminService, FootballPlayerCountryAdminService>();
            services.AddTransient<IFootballSquadAdminService, FootballSquadAdminService>();
            services.AddTransient<IFootballSquadPlayerAdminService, FootballSquadPlayerAdminService>();
            services.AddTransient<IFootballGameAdminService, FootballGameAdminService>();
            services.AddTransient<IFootballCoachAdminService, FootballCoachAdminService>();
            services.AddTransient<IFootballCoachTeamAdminService, FootballCoachTeamAdminService>();
            services.AddTransient<IFootballSquadCoachAdminService, FootballSquadCoachAdminService>();
            services.AddTransient<IFootballGameStatisticAdminService, FootballGameStatisticAdminService>();
            services.AddTransient<IFootballLineUpAdminService, FootballLineUpAdminService>();
            services.AddTransient<IFootballReserveAdminService, FootballReserveAdminService>();
            services.AddTransient<IFootballGoalAdminService, FootballGoalAdminService>();
            services.AddTransient<IFootballGoalTypeAdminService, FootballGoalTypeAdminService>();
            services.AddTransient<IFootballGoalAssistanceAdminService, FootballGoalAssistanceAdminService>();
            services.AddTransient<IFootballCardAdminService, FootballCardAdminService>();
            services.AddTransient<IFootballSidelineAdminService, FootballSidelineAdminService>();
            services.AddTransient<IFootballSubstituteAdminService, FootballSubstituteAdminService>();
            services.AddTransient<IFootballPlayerInAdminService, FootballPlayerInAdminService>();
            services.AddTransient<IFootballPlayerOutAdminService, FootballPlayerOutAdminService>();
            services.AddTransient<IFootballGameRefereeAdminService, FootballGameRefereeAdminService>();
            services.AddTransient<IFootballGameStatisticCoachAdminService, FootballGameStatisticCoachAdminService>();
            services.AddTransient<IFootballStadiumAdminService, FootballStadiumAdminService>();
            services.AddTransient<ISquadService, SquadService>();
            services.AddTransient<ICoachService, CoachService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
