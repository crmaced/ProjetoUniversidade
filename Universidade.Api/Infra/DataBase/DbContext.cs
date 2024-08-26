//using Microsoft.Data.SqlClient;
using Serilog;
using System.Data.Common;
using System.Data.SqlClient;

namespace Universidade.Api.Infra.DataBase;

public class DbContext
{
    public DbContext(string connectionString)
    {
        try
        {
            Id = Guid.NewGuid();
            Connection = new SqlConnection(connectionString);
            Log.Verbose($"Conexão com banco de dados.{Connection?.State.ToString() ?? "Nulo"}");
        }
        catch (Exception ex)
        {
            string Message = $"Não foi possível estabelecer conexão.";
            Log.Error(ex, Message);
            throw new ArgumentException(Message, ex);
        }
    }

    public Guid Id { get; set; }
    public SqlConnection Connection { get; internal set; }
    //public DbTransaction Transaction { get; internal set; }
}
