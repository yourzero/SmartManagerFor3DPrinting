using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Manager_for_3_D_Printing.Services;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public class FileModelImporter(ModelImportService importService) : IFileModelImporter
    {
        public async Task<Model> ImportFromFileAsync(FileResult file)
        {
            await using var stream = await file.OpenReadAsync();
            return await importService.ImportModelAsync(stream, file.FileName, string.Empty);
        }
    }
}
