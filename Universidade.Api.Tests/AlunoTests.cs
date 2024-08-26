using Microsoft.AspNetCore.Mvc;
using Universidade.Api.Controllers;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;
using Universidade.Api.Domain.Repository;
using Universidade.Api.Infra.DataBase;
namespace Universidade.Api.Tests;

public class AlunoTests
{
    private readonly Repository repository = 
        new Repository(new DbContext("Server=DESKTOP-I3TLFU2;DataBase=Universidade;Connection Timeout=9;Max Pool Size=150;Integrated Security=True"));

    AlunoDTO alunoTeste = new AlunoDTO()
    {
        Name = "Joesley Maracau",
        PassWord = "Ajd(5#hskI",
        Username = "joesley.maracau"
	};

    [Fact]
    public async void BuscarAlunos()
    {
        var controller = new AlunoController();

        var apiResponse =
            await controller.GetAllAsync(repository);

        Assert.NotNull(apiResponse);
        var objetctResult = apiResponse.Result as ObjectResult;

        Assert.NotNull(objetctResult);
        Assert.Equal(200, objetctResult.StatusCode);
    }

	[Fact]
	public async void BuscarAluno()
	{
		var controller = new AlunoController();

		var apiResponse =
			await controller.GetAlunoAsync(repository, 2);

		Assert.NotNull(apiResponse);
		var objetctResult = apiResponse.Result as ObjectResult;

		Assert.NotNull(objetctResult);
		Assert.Equal(200, objetctResult.StatusCode);
	}

	[Fact]
    public async void InserirAluno()
    {
        var controller = new AlunoController();

        var apiResponse = await controller.PostAsync(repository, alunoTeste);

        Assert.NotNull(apiResponse);
        Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
        var objetctResult = apiResponse.Result as ObjectResult;

        Assert.NotNull(objetctResult);
        Assert.Equal(201, objetctResult.StatusCode);

        var responseModel = objetctResult.Value as ResponseModel;
        Assert.NotNull(responseModel.Data);
    }

    [Fact]
    public async void AlterarAluno()
    {
        var controller = new AlunoController();

        var apiResponse = await controller.UpdateAsync
            (repository,8, new AlunoDTO()
            {
                Name = "Paloma Santos Silveira",
                Username = "paloma.silv"
            });

        Assert.NotNull(apiResponse);
        var objetctResult = apiResponse.Result as ObjectResult;

        Assert.NotNull(objetctResult);
        Assert.Equal(200, objetctResult.StatusCode);
    }

    [Fact]
    public async void DeletarAluno()
    {
        var controller = new AlunoController();

        var apiResponse = await controller.DeleteAsync(repository, 17);

        Assert.NotNull(apiResponse);
        Assert.IsType<ActionResult<ResponseModel>>(apiResponse);
        var objetctResult = apiResponse.Result as ObjectResult;

        Assert.NotNull(objetctResult);
        Assert.Equal(200, objetctResult.StatusCode);
    }
}