using Attribute = WebApi.DbEntities.Attribute;

namespace WebApi.Services.Interfaces {
    public interface IAttributeService {

        Task FetchAttributesAsync();
        Task<IEnumerable<Attribute>> GetAttributesAsync();
        Task GenerateAttributeDocumentAsync();
    }
}
