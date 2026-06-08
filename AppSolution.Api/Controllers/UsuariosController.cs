using AppSolution.Api.Requests;
using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Application.Feature.Usuarios.Commands.CriarUsuario;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;
using AppSolution.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace AppSolution.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly CriarUsuarioCommandsHandler _criarHandler;
    private readonly ObterUsuariosQueryHandler _obterUsuariosHandler;
    private readonly ObterUsuarioPorIdQueryHandler _obterPorIdHandler;
    private readonly AtualizarEmailUsuarioCommandsHandler _atualizarEmailUsuarioCommandHandler;
    private readonly AtualizarNomeUsuarioCommandsHandler _atualizarNomeUsuarioCommandHandler;
    private readonly ExcluirUsuarioCommandsHandler _excluirUsuarioCommandHandler;

    public UsuariosController(
        CriarUsuarioCommandsHandler criarHandler,
        ObterUsuariosQueryHandler obterUsuariosHandler,
        ObterUsuarioPorIdQueryHandler obterPorIdHandler,
        AtualizarEmailUsuarioCommandsHandler atualizarEmailUsuarioCommandHandler,
        AtualizarNomeUsuarioCommandsHandler atualizarNomeUsuarioCommandHandler,
        ExcluirUsuarioCommandsHandler excluirUsuarioCommandHandler)
    {
        _criarHandler = criarHandler;
        _obterUsuariosHandler = obterUsuariosHandler;
        _obterPorIdHandler = obterPorIdHandler;
        _atualizarEmailUsuarioCommandHandler = atualizarEmailUsuarioCommandHandler;
        _atualizarNomeUsuarioCommandHandler = atualizarNomeUsuarioCommandHandler;
        _excluirUsuarioCommandHandler = excluirUsuarioCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarUsuarioRequests request)
    {
        var command = new CriarUsuarioCommands(request.Nome, request.Email);

        var id = await _criarHandler.Handle(command);

        return CreatedAtAction(
            nameof(ObterPorId),
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarEmail(Guid id, AtualizarEmailUsuarioRequests request)
    {
        var command = new AtualizarEmailUsuarioCommands(
                        id,
                        request.Email);

        await _atualizarEmailUsuarioCommandHandler.Handle(command);

        return NoContent();
    }

    [HttpPut("nome/{id:guid}")]
    public async Task<IActionResult> AtualizarNome(Guid id, AtualizarNomeUsuarioRequests request)
    {
        var command = new AtualizarNomeUsuarioCommands(
                        id,
                        request.Nome);

        await _atualizarNomeUsuarioCommandHandler.Handle(command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ExcluirUsuario(Guid id)
    {
        var command = new ExcluirUsuarioCommands(id);

        await _excluirUsuarioCommandHandler.Handle(command);

        return NoContent();
    }
}