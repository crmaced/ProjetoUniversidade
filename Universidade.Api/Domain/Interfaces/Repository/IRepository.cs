using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;

namespace Universidade.Api.Domain.Interfaces.Repository;

public interface IRepository
{
    Task<ResponseModel> GetAlunosAsync();
    Task<ResponseModel> GetAlunoAsync(int id);
	Task<ResponseModel> PostAlunoAsync(AlunoDTO aluno);
    Task<ResponseModel> DeleteAlunoAsync(int id);
    Task<ResponseModel> UpdateAlunoAsync(int id, AlunoDTO aluno);

    Task<ResponseModel> GetTurmasAsync();
    Task<ResponseModel> GetTurmaAsync(int id);
	Task<ResponseModel> PostTurmaAsync(TurmaDTO turma);
    Task<ResponseModel> DeleteTurmaAsync(int id);
    Task<ResponseModel> UpdateTurmaAsync(int id, TurmaDTO turma);

    Task<ResponseModel> GetRelacionamentosAsync();
    Task<ResponseModel> InsertRelacionamentoAsync(int alunoId, int turmaId);
    Task<ResponseModel> DeleteRelacionamentoAsync(int alunoId, int turmaId);
}
