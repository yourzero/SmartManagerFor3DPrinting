using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public class UrlModelImporter : IModelImporter
    {
        private readonly DatabaseContext database;
        private readonly HttpClient httpClient;

        public UrlModelImporter(DatabaseContext database, HttpClient httpClient)
        {
            this.database = database;
            this.httpClient = httpClient;
        }

        public async Task<Model> ImportFromUrlAsync(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);
            if (string.IsNullOrEmpty(fileName))
                throw new InvalidOperationException("Cannot determine filename from URL.");

            
            var model = new Model
            {
                Name = Path.GetFileNameWithoutExtension(fileName),
                //FilePath = destPath,
                SourceUrl = url,
                DateAdded = DateTime.UtcNow
            };
            
            var modelDir = GenerateModelDirectory(model);

            var destPath = Path.Combine(modelDir, fileName);
            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            await using var fs = File.OpenWrite(destPath);
            await using var stream = await response.Content.ReadAsStreamAsync();
            await stream.CopyToAsync(fs);


            await database.InsertModelAsync(model);

            var fileType = Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant();
            var modelFile = new ModelFile
            {
                ModelId = modelId,
                FilePath = destPath,
                FileType = fileType
            };
            await database.InsertModelFileAsync(modelFile);

            return model;
        }
        
        private static string GenerateModelDirectory(Model model)
        {
            // copy into your library folder
            var libDir = Path.Combine(FileSystem.AppDataDirectory, "Models");
            var modelDir = Path.Combine(libDir, model.Id);
            Directory.CreateDirectory(modelDir);
            return modelDir;
        }
    }
}
