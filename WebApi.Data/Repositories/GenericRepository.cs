using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Webapi.Data.Repositories.Interfaces;
using WebApi.Data;

namespace Webapi.Data.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new() {
        private WebApiContext _chatContext;
        public WebApiContext ChatContext { get => _chatContext; }
        public GenericRepository(WebApiContext chatContext) {
            _chatContext = chatContext;
        }
        public void AddRange(IEnumerable<T> entities) {
            _chatContext.Set<T>().AddRange(entities);
        }
        public async Task<ICollection<T>> GetAllAsync() {
            return await _chatContext.Set<T>().ToListAsync();
        }
        public async Task<ICollection<T>> GetAllNoTrackingAsync() {
            return await _chatContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task SaveAsync() {
            await _chatContext.SaveChangesAsync();
        }
    }
}
