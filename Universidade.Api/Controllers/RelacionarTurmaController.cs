using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.Interfaces.Repository;

namespace Universidade.Api.Controllers
{
    [ApiController]
    [Route("RelacionarTurma")]
    public class RelacionarTurmaController : Controller
    {
        [HttpGet("ListarRelacionamentos")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> ListarRelacionamentosAsync(
            [FromServices] IRepository repository)
        {
            var result = await repository.GetRelacionamentosAsync();
            return StatusCode(result.Code, result);
        }

        [HttpPost("IncluirRelacionamento")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> RelacionarTurmaAsync(
            int alunoId, int turmaId,
            [FromServices] IRepository repository)
        {
            var result = await repository.InsertRelacionamentoAsync(alunoId, turmaId);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("DeletarRelacionamento")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> DeletarRelacionamentoAsync(
            [FromServices] IRepository repository,
            int alunoId, int turmaId)
        {
            var result = await repository.DeleteRelacionamentoAsync(alunoId, turmaId);
            return StatusCode(result.Code, result);
        }
    }
}
