﻿using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["DBConnection:DBConnectionString"];

// Add services to the container.

builder.Services.AddControllers(); 

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql( connection,
    new MySqlServerVersion(new Version(Int32.Parse(builder.Configuration["DBConnection:DBMajorVersion"]),
        Int32.Parse(builder.Configuration["DBConnection:DBMinorVersion"]),
        Int32.Parse(builder.Configuration["DBConnection:DBPatchVersion"])))));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

