using SQLite;
using System.Threading.Tasks;
using Manager_for_3_D_Printing.Models;
using Models;

namespace Manager_for_3_D_Printing.Data
{
    public class DatabaseContext
    {
        public SQLiteAsyncConnection Connection { get; }

        public DatabaseContext(string dbPath)
        {
            Connection = new SQLiteAsyncConnection(dbPath);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<Model>();
            await Connection.CreateTableAsync<ModelFile>();
            await Connection.CreateTableAsync<ModelTag>();
            await Connection.CreateTableAsync<ModelVersion>();
            await Connection.CreateTableAsync<PrintQueueItem>();
        }

        // —— Model CRUD ——

        public Task<int> InsertModelAsync(Model model) =>
            Connection.InsertAsync(model);

        public Task<List<Model>> GetAllModelsAsync() =>
            Connection.Table<Model>().ToListAsync();

        public Task<int> UpdateModelAsync(Model model) =>
            Connection.UpdateAsync(model);

        public Task<int> DeleteModelAsync(Model model) =>
            Connection.DeleteAsync(model);

        // —— ModelFile CRUD ——

        public Task<int> InsertModelFileAsync(ModelFile file) =>
            Connection.InsertAsync(file);

        public Task<List<ModelFile>> GetFilesByModelAsync(string modelId) =>
            Connection.Table<ModelFile>()
                      .Where(f => f.ModelId == modelId)
                      .ToListAsync();

        // —— ModelTag CRUD ——

        public Task<int> InsertModelTagAsync(ModelTag tag) =>
            Connection.InsertAsync(tag);

        public Task<List<ModelTag>> GetTagsByModelAsync(string modelId) =>
            Connection.Table<ModelTag>()
                      .Where(t => t.ModelId == modelId)
                      .ToListAsync();

        // —— ModelVersion CRUD ——

        public Task<int> InsertModelVersionAsync(ModelVersion version) =>
            Connection.InsertAsync(version);

        public Task<List<ModelVersion>> GetVersionsByModelAsync(string modelId) =>
            Connection.Table<ModelVersion>()
                      .Where(v => v.ModelId == modelId)
                      .ToListAsync();

        // —— PrintQueueItem CRUD ——

        public Task<int> EnqueuePrintAsync(PrintQueueItem item) =>
            Connection.InsertAsync(item);

        public Task<List<PrintQueueItem>> GetAllQueueItemsAsync() =>
            Connection.Table<PrintQueueItem>().ToListAsync();

        public Task<int> UpdateQueueItemAsync(PrintQueueItem item) =>
            Connection.UpdateAsync(item);

        public Task<int> DeleteQueueItemAsync(PrintQueueItem item) =>
            Connection.DeleteAsync(item);
    }
}
