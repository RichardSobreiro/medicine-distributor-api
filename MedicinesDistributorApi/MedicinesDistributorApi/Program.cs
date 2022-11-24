using MedicinesDistributorApi.Business;
using MedicinesDistributorApi.Business.IBusiness;
using MedicinesDistributorApi.Repository;
using MedicinesDistributorApi.Repository.DatabaseSettings;
using MedicinesDistributorApi.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MedicineDistributorDatabaseSettings>(
    builder.Configuration.GetSection("MedicineDistributorDatabase"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductsBusiness, ProductsBusiness>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

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
