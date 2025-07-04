using System.Threading.Tasks;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public interface IModelImporter
    {
        Task<Model> ImportFromUrlAsync(string url);
    }
}
