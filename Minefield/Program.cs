using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MineField.Game;
using MineField.Models;
using MineField.Services;
using MineField.Views;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(new GameOptions { MaxLives = 3, NumberOfMines = 32, NumberOfColumns = 8, NumberOfRows = 8 });
        services.AddSingleton<IGameBuilder, GameBuilder>();
        services.AddSingleton<IGameController, GameController>();
        services.AddSingleton<IView, View>();
        services.AddTransient<IHmi, Hmi>();
        services.AddTransient<IUserIo, UserIo>();
        services.AddTransient<IParser<char, Direction>, KeyToMoveParser>();
        services.AddTransient<IConverter<MoveResult, string>, ResultToMessageConverter>();
    }).Build();

host.Services.GetService<IView>()!.Play();
    
