
namespace Webapi.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void AddRange(IEnumerable<T> entities);
        Task SaveAsync();
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllNoTrackingAsync();
    }
}
