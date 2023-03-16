using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Votus.Proposicao.API;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using System.Configuration;
using Microsoft.OpenApi.Models;
using Votus.Common.ServiceBus;
using Votus.Proposicao.API.EventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


builder.Services.AddSwaggerGen(option =>
{
    
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Proposição API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    var filePath = Path.Combine(AppContext.BaseDirectory, "Votus.Proposicao.API.xml");
    option.IncludeXmlComments(filePath);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<ProposicaoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var authenticationOptions = builder.Configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>();

builder.Services.AddKeycloakAuthentication(authenticationOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
