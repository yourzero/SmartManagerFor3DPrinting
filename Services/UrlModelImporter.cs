using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Manager_for_3_D_Printing.Services;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public class UrlModelImporter(ModelImportService importService, HttpClient httpClient) : IUrlModelImporter
    {
        public async Task<Model> ImportFromUrlAsync(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);
            if (string.IsNullOrEmpty(fileName))
                throw new InvalidOperationException("Cannot determine filename from URL.");

            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();

            return await importService.ImportModelAsync(stream, fileName, url);
        }
    }
}
