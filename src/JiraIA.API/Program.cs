using JiraIA.API.Configurations;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Interfaces.Services;
using JiraIA.Domain.Services;
using JiraIA.Infra;
using JiraIA.Infra.Factories;
using JiraIA.Infra.Providers;
using JiraIA.Infra.Repositories;
using JiraIA.Infra.UoW;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStringsProvider>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<DbSettingsProvider>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IMongoClient, MongoClient>(MongoClientFactory.Create);
builder.Services.AddSingleton<JiraIAContext>();
builder.Services.AddScoped(conn => conn.GetService<IMongoClient>().StartSession());

builder.Services.AddAutoMapperSetup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
