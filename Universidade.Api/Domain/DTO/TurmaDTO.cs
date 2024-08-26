using System.ComponentModel.DataAnnotations;

namespace Universidade.Api.Domain.DTO;

public class TurmaDTO
{
    [Required(ErrorMessage = "O nome da turma é obrigatório.", AllowEmptyStrings = false)]
    [StringLength(maximumLength: 45, MinimumLength = 1, ErrorMessage = "Valor máximo do campo é 45 caracteres.")]
    public string? NomeTurma {  get; set; }

    [Required(ErrorMessage = "O ano da turma é obrigatório.", AllowEmptyStrings = false)]
    [Range(1, 5, ErrorMessage = "O ano da turma deve ser entre 1 e 5 anos.")]
    public int? Ano {  get; set; }

    [Required(ErrorMessage = "O Id do curso da turma.", AllowEmptyStrings = false)]
    public int? IdCurso { get; set; }
}
