using WebApi.Services.Interfaces;

namespace WebApi.Services {
    public class FileService : IFileService {

        public string[] GetFiles(string path) {
            string[] files = { };

            if (Directory.Exists(path)) {
                files = Directory.GetFiles(path, @"*.json", SearchOption.TopDirectoryOnly);
            }
            return files;
        }
        public async Task<string> ReadFileAsync(string filePath) {
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
