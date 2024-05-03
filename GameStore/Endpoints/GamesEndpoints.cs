namespace GameStore.Endpoints;
using GameStore.Dtos;
using GameStoreApi.Data;
using Microsoft.AspNetCore.Mvc;
using GameStore.Entities;
using GameStoreApi.Mapping;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new(
            1,
            "Street Fighter",
            "Fighting",
            49.99M,
            new DateOnly(2021, 11, 20)
        ),
        new(
            2,
            "Grand Theft Auto",
            "Arcade",
            23.99M,
            new DateOnly(2015, 09, 12)
        ),
        new(
            3,
            "FIFA",
            "Sports",
            89.99M,
            new DateOnly(2019, 01, 10)
        ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games");

        //Get Games
        group.MapGet("/", () => games);

        //Get Games by Id
        group.MapGet("/{id}", (int id) =>
         {
             GameDto? game = games.Find((game) => game.Id == id);
             if (game == null)
                 return Results.NotFound();
             return Results.Ok(game);

         }).WithName(GetGameEndpointName);


        //Post Game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
         {

             //using the db context to add games to the database
            //  Game game = new(){
            //     Name = newGame.Name,
            //     Genre = dbContext.Genres.Find(newGame.GenreId),
            //     GenreId = newGame.GenreId,
            //     Price = newGame.Price,
            //     ReleaseDate = newGame.ReleaseDate
            //  };
             Game game = newGame.ToEntity();//abstracted the code above to this
             game.Genre = dbContext.Genres.Find(newGame.GenreId);


             dbContext.Games.Add(game);
             dbContext.SaveChanges();

            // GameDto gameDto = new(
            //     game.Id,
            //     game.Name,
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleaseDate
            // );


            // using the dummy data
            //  GameDto game = new(
            //      games.Count + 1,
            //      newGame.Name,
            //      newGame.Genre,
            //      newGame.Price,
            //      newGame.ReleaseDate
            //  );
            // games.Add(game);

            //  return Results.CreatedAtRoute(GetGameEndpoi ntName, new { id = game.Id }, game);
            return Results.CreatedAtRoute(
                GetGameEndpointName, 
                new { id = game.Id },
                game.ToDto());
         });


        //Put Game by Id
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex((game) => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.UpdateDate
            );

            return Results.NoContent();
        });
        // group.MapPut("/{id}", (int id, UpdateGameDto updateGame, GameStoreContext dbContext, Game game) => 
        // {
        //     var index = 
        // });



        //Delete Game by Id
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}