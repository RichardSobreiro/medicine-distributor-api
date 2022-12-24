using Keycloak.AuthServices.Authentication;
using MedicinesDistributorApi.Business;
using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Repository;
using MedicinesDistributorApi.Repository.DatabaseSettings;
using MedicinesDistributorApi.Repository.IRepository;
using MedicinesDistributorApi.Repository.IRepository.IPagSeguro;
using MedicinesDistributorApi.Repository.PagSeguro;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSites",
        policy =>
        {
            policy.WithOrigins("https://localhost:7226").AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("https://localhost:7007").AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("https://mgm-ui.sobreiro.dev").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.Configure<MedicineDistributorDatabaseSettings>(
    builder.Configuration.GetSection("MedicineDistributorDatabase"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductsBusiness, ProductsBusiness>();
builder.Services.AddScoped<IMeasurementUnitsBusiness, MeasurementUnitsBusiness>();
builder.Services.AddScoped<IPaymentsBusiness, PaymentsBusiness>();

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IMeasurementUnitsRepository, MeasurementUnitsRepository>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IPaymentsPagSeguroRepository, PaymentsPagSeguroRepository>();

builder.Services.AddKeycloakAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseCors("AllowSites");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
