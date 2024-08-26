namespace Universidade.Web.Domain.Enties;

public class TurmaDTO
{
	public string? NomeTurma { get; set; }

	public int? Ano { get; set; }

	//[Required(ErrorMessage = "O Id do curso da turma.", AllowEmptyStrings = false)]
	public int? IdCurso { get; set; }
}
