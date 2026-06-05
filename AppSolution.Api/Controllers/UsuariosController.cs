using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Application.Features.Usuarios.Commands.CriarUsuario;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;
using Microsoft.AspNetCore.Mvc;

namespace AppSolution.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly CriarUsuarioCommandHandler _criarHandler;
    private readonly ObterUsuariosQueryHandler _obterUsuariosHandler;
    private readonly ObterUsuarioPorIdQueryHandler _obterPorIdHandler;
    public UsuariosController(
        CriarUsuarioCommandHandler criarHandler,
        ObterUsuariosQueryHandler obterUsuariosHandler,
        ObterUsuarioPorIdQueryHandler obterPorIdHandler)
    {
        _criarHandler = criarHandler;
        _obterUsuariosHandler = obterUsuariosHandler;
        _obterPorIdHandler = obterPorIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarUsuarioCommand command)
    {
        var id = await _criarHandler.Handle(command);

        return CreatedAtAction(
            nameof(Criar),
            new { id },
            id);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _obterUsuariosHandler.Handle(new ObterUsuariosQuery());

        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var usuario = await _obterPorIdHandler.Handle(new ObterUsuarioPorIdQuery(id));

        return Ok(usuario);
    }
}