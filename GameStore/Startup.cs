using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using GameStore.Validator;

namespace GameStoreApi;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // services.AddControllers()
        //     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateGameDtoValidator>());
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
    }
}
