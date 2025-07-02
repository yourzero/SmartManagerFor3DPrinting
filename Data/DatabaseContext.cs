using Manager_for_3_D_Printing.Models;
using Models;
using SQLite;

namespace Manager_for_3_D_Printing.Data;

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
