using Microsoft.AspNetCore.Mvc;
using Universidade.Api.Controllers;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.Repository;
using Universidade.Api.Infra.DataBase;

namespace Universidade.Api.Tests;

public class RelacionamentoTests
{
	private readonly Repository repository =
		new Repository(new DbContext("Server=DESKTOP-I3TLFU2;DataBase=Universidade;Connection Timeout=9;Max Pool Size=150;Integrated Security=True"));

	private RelacionarTurmaController controller = new RelacionarTurmaController();

	[Fact]
	public async void ListarRelacionamentos()
	{
		var apiResponse =
			await controller.ListarRelacionamentosAsync(repository);

		Assert.NotNull(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}

	[Fact]
	public async void IncluirRelacionamento()
	{
		var apiResponse = await controller.RelacionarTurmaAsync(alunoId: 18,turmaId: 6,repository);

		Assert.NotNull(apiResponse);
		Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(201, objetctResult.StatusCode);

		var responseModel = objetctResult.Value as ResponseModel;
	}

	[Fact]
	public async void DeletarRelacionamento()
	{
		var apiResponse = await controller.DeletarRelacionamentoAsync(repository, alunoId: 18, turmaId: 6);

		Assert.NotNull(apiResponse);
		Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}
}
