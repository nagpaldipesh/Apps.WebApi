
using WebApi.DbEntities;

namespace WebApi.Services.Interfaces {
    public interface IPageService {

        Task FetchPages();

        Task<IEnumerable<Page>> GetPages();
    }
}
