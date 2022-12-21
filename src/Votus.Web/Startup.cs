using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using Votus.Application.Services;
using Votus.Domain.Repositories;
using Votus.Domain.Services;
using Votus.Infrastructure;
using Votus.Infrastructure.Data;
using Votus.Infrastructure.Data.Repositories;
using Votus.Infrastructure.Email.Providers.SendGrid;
using Votus.Infrastructure.Identity;
using Votus.Web.ApiClients;

using Votus.Web.Models;

namespace Votus.Web
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


            services.AddDbContext<ApplicationDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Votus.Infrastructure.Data")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<WebApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            services
                .AddRefitClient<IVotusApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:6001"));


#warning debt: use assembly scanning
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPropositionRepository, PropositionRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPropositionService, PropositionService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new[] { "pt-BR" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseCors(VotusApiSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
