namespace Universidade.Api.CrossCutting.Configuration;

public class DataBaseConfiguration
{
    public const string Key = "DataBase";
    public string? Server {  get; set; }
    public string? Database { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? AdditionalConfigurations { get; set; }

    public override string ToString()
    {
        return string.Format("Server={0};DataBase={1};User Id={2};Password={3};{4};", Server, Database, UserId, Password, AdditionalConfigurations);
    }
}
