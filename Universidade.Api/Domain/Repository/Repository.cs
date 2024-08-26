using Universidade.Api.Domain.Enties;
using Dapper;
using Universidade.Api.Domain.Interfaces.Repository;
using Universidade.Api.CrossCutting.Models;
using Universidade.Api.Domain.DTO;
using Serilog;
using Universidade.Api.Infra.DataBase;
using System.Data;

namespace Universidade.Api.Domain.Repository;

public class Repository : IRepository
{
    protected readonly DbContext _context;

    public Repository(DbContext context) => _context = context;

    public async Task<ResponseModel> GetAlunosAsync()
    {
        try
        {
            string sql = $"SELECT id, usuario, nome FROM aluno";

            var result = await _context.Connection.QueryAsync<Aluno>(sql: sql, commandType: CommandType.Text);

            return result.Any() ? Responses.Ok(result.ToList()) : Responses.NotFound();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

	public async Task<ResponseModel> GetAlunoAsync(int id)
	{
		try
		{
			string sql = $"SELECT id, usuario, nome FROM aluno WHERE id = @id";

			var result = await _context.Connection.QueryFirstOrDefaultAsync<Aluno>(
                sql: sql, param: new { id }, commandType: CommandType.Text);

			return result != null ? Responses.Ok(result) : Responses.NotFound();
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Erro no banco de dados.");
			return Responses.InternalServerError();
		}
	}

	public async Task<ResponseModel> PostAlunoAsync(AlunoDTO aluno)
    {
        try
        {
            aluno.PassWord = BCrypt.Net.BCrypt.HashPassword(aluno.PassWord);

            string sql = $"IF NOT EXISTS (SELECT 1 FROM aluno WHERE usuario = @usuario) " +
                $"BEGIN INSERT INTO aluno (nome, usuario, senha) " +
                $"VALUES (@nome, @usuario, @senha); " +
                $"SELECT SCOPE_IDENTITY() as Id; " +
                $"END";

            var parameters = new DynamicParameters();
            parameters.Add("@nome", aluno.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@usuario", aluno.Username, DbType.String, ParameterDirection.Input);
            parameters.Add("@senha", aluno.PassWord, DbType.String, ParameterDirection.Input);

            var id = await _context.Connection.QuerySingleOrDefaultAsync<int?>
                (sql: sql, param: parameters, commandType: CommandType.Text);

            var alunoCriado = id.HasValue ?
                new Aluno() { id =  id.Value, nome = aluno.Name, usuario = aluno.Username } : null;

            return alunoCriado is not null ?
                Responses.Created("Dados inseridos com sucesso.", alunoCriado) :
                Responses.BadRequest("Não foi possível inserir os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> DeleteAlunoAsync(int id)
    {
        try
        {
            string sql = $"DELETE FROM aluno WHERE id = @id";

            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: new {id}, commandType: CommandType.Text);

            return result == 1 ? Responses.Ok("Dados deletados com sucesso.") :
                Responses.BadRequest("Não foi possível apagar os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> UpdateAlunoAsync(int id, AlunoDTO aluno)
    {
        try
        {
            string atualizaSenha = string.Empty;

			if (aluno.PassWord is not null)
            {
                aluno.PassWord = BCrypt.Net.BCrypt.HashPassword(aluno.PassWord);
				atualizaSenha = ", senha = @senha";
			}

            string sql = $"IF EXISTS (SELECT 1 FROM aluno WHERE id = @id) " +
                $"BEGIN UPDATE aluno SET nome = @nome, usuario = @usuario{atualizaSenha} WHERE id = @id END";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@nome", aluno.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@usuario", aluno.Username, DbType.String, ParameterDirection.Input);
            parameters.Add("@senha", aluno.PassWord, DbType.String, ParameterDirection.Input);

            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: parameters, commandType: CommandType.Text);

            return result == 1 ? Responses.Ok("Dados alterados com sucesso.") :
                Responses.BadRequest("Não foi possível alterar os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> GetTurmasAsync()
    {
        try
        {
            string sql = $"SELECT id, curso_id, turma, ano FROM turma";

            var result = await _context.Connection.QueryAsync<Turma>(sql: sql, commandType: CommandType.Text);

            return result.Any() ? Responses.Ok(result.ToList()) : Responses.NotFound();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

	public async Task<ResponseModel> GetTurmaAsync(int id)
	{
		try
		{
			string sql = $"SELECT id, curso_id, turma, ano FROM turma WHERE id = @id";

			var result = await _context.Connection.QueryFirstOrDefaultAsync<Turma>(
				sql: sql, param: new { id }, commandType: CommandType.Text);

			return result != null ? Responses.Ok(result) : Responses.NotFound();
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Erro no banco de dados.");
			return Responses.InternalServerError();
		}
	}

	public async Task<ResponseModel> PostTurmaAsync(TurmaDTO turma)
    {
        try
        {
            string sql = $"IF NOT EXISTS (SELECT 1 FROM turma WHERE turma = @turma) " +
                $"BEGIN INSERT INTO turma (curso_id, turma, ano) " +
                $"VALUES (@curso_id, @turma, @ano) " +
                $"SELECT SCOPE_IDENTITY() as Id; " +
                $"END";

            var parameters = new DynamicParameters();
            parameters.Add("@curso_id", turma.IdCurso, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@turma", turma.NomeTurma, DbType.String, ParameterDirection.Input);
            parameters.Add("@ano", turma.Ano, DbType.Int64, ParameterDirection.Input);

            var id = await _context.Connection.QuerySingleOrDefaultAsync<int?>
                (sql: sql, param: parameters, commandType: CommandType.Text);

            var turmaCriada = id.HasValue ?
                new Turma() { id =  id.Value, turma = turma.NomeTurma, ano = (int)turma.Ano, curso_id = (int)turma.IdCurso } 
                : null;

            return turmaCriada is not null ?
                Responses.Created("Dados inseridos com sucesso.", turmaCriada) :
                Responses.BadRequest("Não foi possível inserir os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> DeleteTurmaAsync(int id)
    {
        try
        {
            string sql = $"DELETE FROM turma WHERE id = @id";

            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: new { id }, commandType: CommandType.Text);

            return result == 1 ? Responses.Ok("Dados deletados com sucesso.") :
                Responses.BadRequest("Não foi possível apagar os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> UpdateTurmaAsync(int id, TurmaDTO turma)
    {
        try
        {
            //verificar aqui 
            string sql = $"IF EXISTS (SELECT 1 FROM turma WHERE id = @id) " +
                $"BEGIN UPDATE turma SET curso_id = @curso_id, turma = @turma, ano = @ano WHERE id = @id END";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@curso_id", turma.IdCurso, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@turma", turma.NomeTurma, DbType.String, ParameterDirection.Input);
            parameters.Add("@ano", turma.Ano, DbType.Int64, ParameterDirection.Input);

            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: parameters, commandType: CommandType.Text);

            return result == 1 ? Responses.Ok("Dados alterados com sucesso.") :
                Responses.BadRequest("Não foi possível alterar os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> GetRelacionamentosAsync()
    {
        try
        {
            string sql = $"SELECT id, turma FROM turma";

            var result = await _context.Connection.QueryAsync<Relacionamento>
                (sql: sql, commandType: CommandType.Text);

            foreach (var turma in result)
            {
                string sqlAluno = $"SELECT a.id, a.nome, a.usuario " +
                    $"FROM aluno a " +
                    $"LEFT JOIN aluno_turma b ON b.aluno_id = a.id " +
                    $"WHERE b.turma_id = @turma_id ";

                var parameters = new DynamicParameters();
                parameters.Add("@turma_id", turma.id, DbType.Int64, ParameterDirection.Input);
                
                var response = await _context.Connection.QueryAsync<Aluno>
                    (sql: sqlAluno, param: parameters, commandType: CommandType.Text);

                turma.alunos = response.ToList();
            }

            return result.Any() ? Responses.Ok(result.ToList()) : Responses.NotFound();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> InsertRelacionamentoAsync(int alunoId, int turmaId)
    {
        try
        {
            string sql = $"IF NOT EXISTS (SELECT 1 FROM aluno_turma WHERE aluno_id = @aluno_id AND turma_id = @turma_id) " +
                $"BEGIN INSERT INTO aluno_turma (aluno_id, turma_id) VALUES (@aluno_id, @turma_id) END";

            var parameters = new DynamicParameters();
            parameters.Add("@aluno_id", alunoId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@turma_id", turmaId, DbType.Int64, ParameterDirection.Input);
          
            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: parameters, commandType: CommandType.Text);

            return result == 1 ? Responses.Created("Dados inseridos com sucesso.") :
                Responses.BadRequest("Não foi possível inserir os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }

    public async Task<ResponseModel> DeleteRelacionamentoAsync(int alunoId, int turmaId)
    {
        try
        {
            string sql = $"DELETE FROM aluno_turma WHERE aluno_id = @aluno_id AND turma_id = @turma_id";

            var parameters = new DynamicParameters();
            parameters.Add("@aluno_id", alunoId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@turma_id", turmaId, DbType.Int64, ParameterDirection.Input);

            var result = await _context.Connection.ExecuteAsync
                (sql: sql, param: parameters, commandType: CommandType.Text);

            return result == 1 ? Responses.Ok("Dados deletados com sucesso.") :
                Responses.BadRequest("Não foi possível apagar os dados.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no banco de dados.");
            return Responses.InternalServerError();
        }
    }
}
