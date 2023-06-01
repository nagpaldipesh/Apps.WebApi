
using WebApi.DbEntities;

namespace WebApi.Services.Interfaces {
    public interface IPageService {

        Task FetchPagesAsync();

        Task<IEnumerable<Page>> GetPagesAsync();

        Task GeneratePagesDocumentAsync();

    }
}
