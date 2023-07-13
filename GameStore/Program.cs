using GameStore.Data;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration
                           .GetConnectionString("GameDbConnection")
                           ?? throw new InvalidOperationException("Connection string 'GameDbConnection' not found."); ;

// Add services to the container.

builder.Services.AddHttpContextAccessor();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
//builder.Services.RegisterMaps();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
builder.Services.AddDbContext<GameStoreContext>(options =>
{
    options.UseSqlServer(connString);
});

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DbContext, GameStoreContext>();
builder.Services.AddScoped<IDbFactory, DbFactory>();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IGameGenreRepository, GameGenreRepository>();
builder.Services.AddScoped<IGameGenreService, GameGenreService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Game}/{action=Index}/{id?}");

app.Run();
