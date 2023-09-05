using CoreBanking.Repository;
using CoreBanking.Repository.Context;
using CoreBanking.Repository.Interfaces;
using CoreBanking.Service;
using CoreBanking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CoreBankingContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection",
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: new List<int>() { 19 });
        });
});

builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

builder.Services.AddLogging(builder =>
{
    builder.AddConfiguration(configuration.GetSection("Logging"))
           .AddConsole();
});

// Add services to the container.

builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
