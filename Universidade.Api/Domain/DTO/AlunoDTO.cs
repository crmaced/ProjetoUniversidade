using System.ComponentModel.DataAnnotations;

namespace Universidade.Api.Domain.DTO;

public class AlunoDTO
{
    [Required(ErrorMessage = "O nome do aluno é obrigatório.", AllowEmptyStrings = false)]
    [StringLength(maximumLength: 255, MinimumLength = 1, ErrorMessage = "Valor máximo do campo é 255 caracteres.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O usuário do aluno é obrigatório.", AllowEmptyStrings = false)]
    [StringLength(maximumLength: 45, MinimumLength = 1, ErrorMessage = "Valor máximo do campo é 45 caracteres.")]
    public string? Username { get; set; }

    public string? PassWord { get; set; }
}
