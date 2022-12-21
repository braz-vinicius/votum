using System;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Votus.Application;
using Votus.Application.Services;
using Votus.Domain.Repositories;
using Votus.Domain.Services;
using Votus.Infrastructure.Data;
using Votus.Infrastructure.Data.Repositories;
using Votus.Infrastructure.Email.Providers.SendGrid;
using Votus.Infrastructure.Storage.Providers;

namespace Votus.Api
{
    public class Startup
    {
        readonly string VotusApiSpecificOrigins = "_votusApiSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: VotusApiSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:6000", "https://localhost:6001", "https://localhost:5001");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Votus.Infrastructure.Data")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Votus.WebApi", Version = "v1" });
            });

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient(x => new BlobServiceClient(Configuration["ExternalProviders:Storage:AzureBlob:ConnectionString"]));
            services.AddTransient<IStorageClient, AzureStorageClient>();
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Votus.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(VotusApiSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
