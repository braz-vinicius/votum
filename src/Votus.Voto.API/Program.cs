using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Votus.Common.ServiceBus;
using Votus.Voto.API;
using Votus.Voto.API.Event;
using Votus.Voto.API.EventHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHostedService<WorkerServiceBus<ProposicaoChangedEventHandler, ProposicaoChangedEvent>>();
builder.Services.AddHostedService<WorkerServiceBus<PessoaChangedEventHandler, PessoaChangedEvent>>();

builder.Services.AddSwaggerGen((Action<Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions>)(option =>
{
    ConfigureSwagger(option);
}));

var authenticationOptions = builder.Configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>();

builder.Services.AddKeycloakAuthentication(authenticationOptions);

builder.Services.AddDbContext<VotoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddTransient<PessoaChangedEventHandler>();
builder.Services.AddTransient<ProposicaoChangedEventHandler>();

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

static void ConfigureSwagger(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions option)
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Voto API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    var filePath = Path.Combine(AppContext.BaseDirectory, "Votus.Voto.API.xml");
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
}