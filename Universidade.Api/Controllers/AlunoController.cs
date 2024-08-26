using FluentValidator.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;
using Universidade.Api.Domain.Enties;
using Universidade.Api.Domain.Interfaces.Repository;

namespace Universidade.Api.Controllers
{
    [ApiController]
    [Route("Aluno")]
    public class AlunoController : Controller
    {
        [HttpGet("BuscarAlunos")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> GetAllAsync(
            [FromServices] IRepository repository)
        {
            var result = await repository.GetAlunosAsync();
            return StatusCode(result.Code, result);
        }

		[HttpGet("BuscarAluno")]
		[Produces(MediaTypeNames.Application.Json)]
		public async Task<ActionResult<ResponseModel>> GetAlunoAsync(
			[FromServices] IRepository repository,
            int id)
		{
			var result = await repository.GetAlunoAsync(id);
			return StatusCode(result.Code, result);
		}

		[HttpPost("InserirAluno")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> PostAsync(
            [FromServices] IRepository repository,
            [FromBody] AlunoDTO aluno)
        {
            var contract = new ValidationContract();

            if(aluno.PassWord is not null)
            {
				string message = "Senha deve ter pelo menos 8 caracteres e incluir letras maiúsculas, minúsculas, números e caracteres especiais.";

				contract.Requires()
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[A-Z]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[a-z]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[0-9]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[^a-zA-Z0-9]"), "Password", message);
			}
            else
			{
                contract.AddNotification("Password", "A senha é obrigatória.");
			}

            if (!contract.Valid)
                return StatusCode(404, Responses.BadRequest(contract.Notifications.ToList()));

            var result = await repository.PostAlunoAsync(aluno);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("DeletarAluno")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> DeleteAsync(
            [FromServices] IRepository repository,
            int id)
        {
            var result = await repository.DeleteAlunoAsync(id);
            return StatusCode(result.Code, result);
        }

        [HttpPatch("AtualizarAluno")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseModel>> UpdateAsync(
            [FromServices] IRepository repository,
            int id,
            [FromBody] AlunoDTO aluno)
        {
            if(aluno.PassWord is not null)
            {
				var contract = new ValidationContract();

				string message = "Senha deve ter pelo menos 8 caracteres e incluir letras maiúsculas, minúsculas, números e caracteres especiais.";

				contract.Requires()
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[A-Z]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[a-z]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[0-9]"), "Password", message)
					.IsTrue(Regex.IsMatch(aluno.PassWord, "[^a-zA-Z0-9]"), "Password", message);

				if (!contract.Valid)
					return StatusCode(404, Responses.BadRequest(message));
			}

			var result = await repository.UpdateAlunoAsync(id, aluno);
            return StatusCode(result.Code, result);
        }
    }
}
