using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Universidade.Api.Controllers;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;
using Universidade.Api.Domain.Repository;
using Universidade.Api.Infra.DataBase;
using Xunit.Sdk;

namespace Universidade.Api.Tests;

public class TurmaTests
{
	private readonly Repository repository =
		new Repository(new DbContext("Server=DESKTOP-I3TLFU2;DataBase=Universidade;Connection Timeout=9;Max Pool Size=150;Integrated Security=True"));

	private TurmaController controller = new TurmaController();

	TurmaDTO inserir = new TurmaDTO()
	{
		NomeTurma = "Sistemas de engenharia - 98262H",
		Ano = 1,
		IdCurso = 45
	};

	TurmaDTO alterar = new TurmaDTO()
	{
		NomeTurma = "Finanças Públicas - 827ER",
		Ano = 3,
		IdCurso = 2
	};

	[Fact]
	public async void BuscarTurmas()
	{
		var apiResponse =
			await controller.GetAllAsync(repository);

		Assert.NotNull(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}

	[Fact]
	public async void BuscarTurma()
	{
		var apiResponse =
			await controller.GetTurmaAsync(repository, 3);

		Assert.NotNull(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}

	[Fact]
	public async void InserirTurma()
	{
		var apiResponse = await controller.PostAsync(repository, inserir);

		Assert.NotNull(apiResponse);
		Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(201, objetctResult.StatusCode);

		var responseModel = objetctResult.Value as ResponseModel;
		Assert.NotNull(responseModel.Data);
	}

	[Fact]
	public async void AlterarTurma()
	{
		var apiResponse = await controller.UpdateAsync(repository, 2, alterar);

		Assert.NotNull(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}

	[Fact]
	public async void DeletarTurma()
	{
		var apiResponse = await controller.DeleteAsync(repository, 4);

		Assert.NotNull(apiResponse);
		Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}
}
