using BankSystem.App.Services;
using FluentValidation.AspNetCore;
using BankSystem.Data;
using BankSystem.App;
using BankSystem.App.Validations;
//using BankSystem.App.Interfaces;
using BankSystem.Data.Storages;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var asss = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddAutoMapper(asss);

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientStorage, ClientStorageEF>();
//builder.Services.AddScoped<ClientService>();
//builder.Services.AddScoped<ClientService>();

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssemblyContaining<ClientDtoValidator>();
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
