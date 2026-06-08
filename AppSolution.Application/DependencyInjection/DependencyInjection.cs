
using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Application.Feature.Usuarios.Commands.CriarUsuario;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;
using Microsoft.Extensions.DependencyInjection;

namespace AppSolution.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CriarUsuarioCommandsHandler>();
        services.AddScoped<ObterUsuariosQueryHandler>();
        services.AddScoped<ObterUsuarioPorIdQueryHandler>();
        services.AddScoped<AtualizarUsuarioCommandsHandler>();
        services.AddScoped<AtualizarEmailUsuarioCommandsHandler>();
        services.AddScoped<AtualizarNomeUsuarioCommandsHandler>();
        services.AddScoped<ExcluirUsuarioCommandsHandler>();

        return services;
    }
}