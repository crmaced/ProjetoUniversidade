using System.ComponentModel.DataAnnotations;

namespace Universidade.Web.Domain.Enties;

public class AlunoDTO
{
	public string? Name { get; set; }

	public string? Username { get; set; }

	public string? PassWord { get; set; }
}
