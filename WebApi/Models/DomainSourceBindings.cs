using Attribute = WebApi.DbEntities.Attribute;

namespace WebApi.Models {

    public class DomainModels {
        public ICollection<DomainSourceBinding> DomainSourceBindings { get; set; }
    }
    public class DomainSourceBinding {
        public Entity? Entity { get; set; }
    }

    public class Entity {

        public string Name { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
    }
}
