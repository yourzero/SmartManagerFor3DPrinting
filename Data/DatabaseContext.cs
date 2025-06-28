
using SQLite;
using Models;

namespace Data;

public class DatabaseContext
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseContext(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Model>().Wait();
        _database.CreateTableAsync<ModelFile>().Wait();
        _database.CreateTableAsync<ModelVersion>().Wait();
        _database.CreateTableAsync<PrintQueueItem>().Wait();
    }

    public SQLiteAsyncConnection Connection => _database;
    
    public Task<int> InsertModelAsync(Model model) =>
        _database.InsertAsync(model);
}
