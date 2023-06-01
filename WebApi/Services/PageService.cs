using System.Text.Json;
using Webapi.Data.Repositories.Interfaces;
using WebApi.DbEntities;
using WebApi.Services.Interfaces;

namespace WebApi.Services {
    public class PageService : IPageService {
        readonly string _filePath;
        private IFileService _fileService;
        private int batchSize;
        private IGenericRepository<Page> _pageRepository;

        public PageService(IFileService fileService, IConfiguration configuration, IGenericRepository<Page> pageRepository) {
            _fileService = fileService;
            _filePath = configuration["PagesFilePath"];
            _pageRepository = pageRepository;
        }

        public async Task FetchPages() {
            HashSet<Page> pages = new HashSet<Page>();
            var filesPath = _fileService.GetFiles(_filePath);
            var tasks = new List<Task<string>>();

            var fileFetchingTask = Parallel.ForEachAsync(filesPath,async (filePath, state) => {
                var file = await _fileService.ReadFileAsync(filePath);

                if (file != null) {
                    JsonDocument jsonDoc = JsonDocument.Parse(file);
                    JsonElement jsonRoot = jsonDoc.RootElement;
                    string title = jsonRoot.GetProperty("Title").GetString();
                    if (title.ToLower().Contains("dashboard")) {
                        string code = jsonRoot.GetProperty("Code").GetString();
                        pages.Add(new Page() {
                            Code = code,
                            Title = title
                        });
                    }
                }
            });

            fileFetchingTask.Wait();

            await SavePagesIntoDatabase(pages);
        }

        public async Task<IEnumerable<Page>> GetPages() {
            return await _pageRepository.GetAllNoTrackingAsync();
        }

        private async Task SavePagesIntoDatabase(IEnumerable<Page> pages) {
            _pageRepository.AddRange(pages);
            await _pageRepository.SaveAsync();
        }


    }
}
