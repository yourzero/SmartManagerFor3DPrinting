using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public interface IFileModelImporter
    {
        Task<Model> ImportFromFileAsync(FileResult file);
    }
}
