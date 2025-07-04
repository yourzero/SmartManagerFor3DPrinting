using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Manager_for_3_D_Printing.Data;
using Manager_for_3_D_Printing.Encryption;
using Manager_for_3_D_Printing.Models;

namespace Manager_for_3_D_Printing.Services
{
    public class ModelImportService
    {
        private readonly DatabaseContext database;

        public ModelImportService(DatabaseContext database)
        {
            this.database = database;
        }

        public async Task<Model> ImportModelAsync(Stream sourceStream, string fileName, string sourceUrl = null)
        {
            var model = new Model
            {
                Name = Path.GetFileNameWithoutExtension(fileName),
                SourceUrl = sourceUrl,
                DateAdded = DateTime.UtcNow
            };

            var modelDir = GenerateModelDirectory(model);
            var destPath = await WriteFileToLibrary(sourceStream, fileName, modelDir);

            model.FileHash = Hashing.ComputeSha256Hash(sourceStream);
            model.FullRootFolderPath = modelDir;
            model.FolderName = model.Id;
            model.FileName = destPath;
            
            await database.InsertModelAsync(model);

            var modelFile = new ModelFile
            {
                ModelId = model.Id,
                FilePath = destPath,
                FileType = Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant()
            };

            await database.InsertModelFileAsync(modelFile);

            return model;
        }

        private static async Task<string> WriteFileToLibrary(Stream sourceStream, string fileName, string modelDir)
        {
            var dest = Path.Combine(modelDir, fileName);
            await using var dst = File.OpenWrite(dest);
            await sourceStream.CopyToAsync(dst);
            return dest;
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
