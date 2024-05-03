using GameStore.Data;
using GameStore.Endpoints;
using GameStoreApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(String.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection String is not provided");
}
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();














































































