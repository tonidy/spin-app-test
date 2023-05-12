using SpinGameApp;
using SpinGameApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connString = builder.Configuration.GetConnectionString("Mongodb");
var dbName = builder.Configuration.GetValue<string>("App:DbName");
builder.Services.AddScoped<MongoDbSettings>(_ => new MongoDbSettings(connString!, dbName!));
builder.Services.AddScoped<ISpinGameRepository, SpinGameRepository>();
builder.Services.AddScoped<ISpinResultRepository, SpinResultRepository>();

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

app.MapGet("/", () => "Welcome to Spin Game!");

app.Run();

public partial class Program { }
