using BankSystem.App.Services;
using FluentValidation.AspNetCore;
using BankSystem.Data.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<IStorage>();

//builder.Services.AddFluentValidation(conf =>
//{
//    conf.RegisterValidatorsFromAssemblyContaining<>();
//});



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
