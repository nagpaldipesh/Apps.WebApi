namespace WebApi.Services.Interfaces {
    public interface IFileService {

        string[] GetFiles(string path);
        Task<string> ReadFileAsync(string filePath);
    }
}
