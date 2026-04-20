namespace DefaultNamespace;

using Microsoft.Data.SqlClient;

public class Db
{
    private readonly string _connectionString;
    
    public Db(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default");
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}