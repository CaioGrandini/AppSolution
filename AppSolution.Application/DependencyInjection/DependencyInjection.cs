using AppSolution.Application.Features.Usuarios.Commands.CriarUsuario;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;
using Microsoft.Extensions.DependencyInjection;

namespace AppSolution.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CriarUsuarioCommandHandler>();
        services.AddScoped<ObterUsuariosQueryHandler>();
        services.AddScoped<ObterUsuarioPorIdQueryHandler>();

        return services;
    }
}