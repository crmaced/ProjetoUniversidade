namespace Universidade.Web.Domain.Enties;

public class Relacionamento
{
	public int id { get; set; }
	public string turma { get; set; }
	public List<Aluno> alunos { get; set; }
}
