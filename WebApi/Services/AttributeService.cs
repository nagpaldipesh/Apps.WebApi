using System.Text.Json;
using Webapi.Data.Repositories.Interfaces;
using WebApi.Models;
using WebApi.Services.Interfaces;
using Attribute = WebApi.DbEntities.Attribute;

namespace WebApi.Services {
    public class AttributeService : IAttributeService {
        readonly string _filePath;

        private IFileService _fileService;
        private IGenericRepository<Attribute> _attributeRepository;
        public AttributeService(IFileService fileService, IConfiguration configuration, IGenericRepository<Attribute> attributeRepository) {
            _fileService = fileService;
            _filePath = configuration["AttributeFilePath"];
            _attributeRepository = attributeRepository;
        }
        public async Task FetchAttributesAsync() {
            var filesPath = _fileService.GetFiles(_filePath);

            HashSet<Attribute> attributes = new HashSet<Attribute>();

            List<String> results = new List<string>();
            var filesTask = Parallel.ForEachAsync(filesPath, async (file, state) => {
                var result = await _fileService.ReadFileAsync(file);
                results.Add(result);
            });

            filesTask.Wait();

            var filesProcessingTask = Parallel.ForEachAsync(results, async (file, state) => {
                if (!string.IsNullOrWhiteSpace(file)) {
                    var domainModels = JsonSerializer.Deserialize<DomainModels>(file);
                    if (domainModels != null) {
                        var domainSourceBindings = domainModels.DomainSourceBindings;
                        if (domainSourceBindings != null) {
                            var jsonAttributes = domainSourceBindings.Select(sel => sel.Entity?.Attributes).ToList();
                            if (jsonAttributes != null) {
                                jsonAttributes.ForEach(attrs => {
                                    if (attrs != null) {
                                        foreach (var attribute in attrs) {
                                            attributes.Add(attribute);
                                        }
                                    }
                                });
                            }
                        }
                    }
                }
            });
            filesProcessingTask.Wait();

            await SaveAttributesIntoDatabase(attributes);
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync() {
            return await _attributeRepository.GetAllNoTrackingAsync();

        }

        private async Task SaveAttributesIntoDatabase(IEnumerable<Attribute> attributes) {
            _attributeRepository.AddRange(attributes);

            await _attributeRepository.SaveAsync();
        }
    }
}
