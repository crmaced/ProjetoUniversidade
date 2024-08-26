using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;
using Universidade.Api.Domain.Interfaces.Repository;

namespace Universidade.Api.Controllers;

[ApiController]
[Route("Turma")]
public class TurmaController : Controller
{
    [HttpGet("BuscarTurmas")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseModel>> GetAllAsync(
            [FromServices] IRepository repository)
    {
        var result = await repository.GetTurmasAsync();
        return StatusCode(result.Code, result);
    }

	[HttpGet("BuscarTurma")]
	[Produces(MediaTypeNames.Application.Json)]
	public async Task<ActionResult<ResponseModel>> GetTurmaAsync(
			[FromServices] IRepository repository,
			int id)
	{
		var result = await repository.GetTurmaAsync(id);
		return StatusCode(result.Code, result);
	}

	[HttpPost("InserirTurma")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseModel>> PostAsync(
        [FromServices] IRepository repository,
        [FromBody] TurmaDTO turma)
    {

        var result = await repository.PostTurmaAsync(turma);
        return StatusCode(result.Code, result);
    }

    [HttpDelete("DeletarTurma")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseModel>> DeleteAsync(
        [FromServices] IRepository repository,
        int id)
    {
        var result = await repository.DeleteTurmaAsync(id);
        return StatusCode(result.Code, result);
    }

    [HttpPatch("AtualizarTurma")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseModel>> UpdateAsync(
        [FromServices] IRepository repository,
        int id,
        [FromBody] TurmaDTO turma)
    {
        var result = await repository.UpdateTurmaAsync(id, turma);
        return StatusCode(result.Code, result);
    }
}
