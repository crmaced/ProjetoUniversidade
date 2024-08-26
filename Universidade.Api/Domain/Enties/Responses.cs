using Universidade.Api.CrossCutting.Models;

namespace Universidade.Api.Domain.Enties;

public static class Responses
{
    public static ResponseModel Ok(object data)
    {
        return new ResponseModel()
        {
            Code = 200,
            Success = true,
            Data = data
        };
    }

    public static ResponseModel Ok(string message)
    {
        return new ResponseModel()
        {
            Code = 200,
            Success = true,
            Message = message
        };
    }

    public static ResponseModel Created(string message, object data)
    {
        return new ResponseModel()
        {
            Code = 201,
            Success = true,
            Message = message,
            Data = data
        };
    }

	public static ResponseModel Created(string message)
	{
		return new ResponseModel()
		{
			Code = 201,
			Success = true,
			Message = message
        };
	}

	public static ResponseModel NotFound()
    {
        return new ResponseModel()
        {
            Code = 404,
            Success = false,
            Message = "Não há dados."
        };
    }

    public static ResponseModel BadRequest(string message)
    {
        return new ResponseModel()
        {
            Code = 400,
            Success = false,
            Message = message
        };
    }
	public static ResponseModel BadRequest(object data)
	{
		return new ResponseModel()
		{
			Code = 400,
			Success = false,
			Data = data
		};
	}

	public static ResponseModel InternalServerError()
    {
        return new ResponseModel()
        {
            Code = 500,
            Success = false,
            Message = "Erro interno na aplicação."
        };
    }
}
