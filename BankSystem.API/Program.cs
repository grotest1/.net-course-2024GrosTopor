using BankSystem.App.Services;
using FluentValidation.AspNetCore;
using BankSystem.App.Validations;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var builder1 = new ConfigurationBuilder();
// установка пути к текущему каталогу
builder1.SetBasePath(Directory.GetCurrentDirectory());
// получаем конфигурацию из файла appsettings.json
builder1.AddJsonFile("appsettings.json");
// создаем конфигурацию
var config = builder1.Build();
// получаем строку подключения
var connectionString = config.GetConnectionString("DefaultConnection");



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<BankSystemDbContext>(options =>
{
    options.UseNpgsql(connectionString, null);
});

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IClientStorage, ClientStorageEF>();
builder.Services.AddScoped<IStorage<Employee>, EmployeeStorageEF>();


builder.Services.AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<ClientDtoValidator>());

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
