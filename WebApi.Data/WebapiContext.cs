using Microsoft.EntityFrameworkCore;
using WebApi.DbEntities;
using Attribute = WebApi.DbEntities.Attribute;

namespace WebApi.Data {
    public class WebApiContext : DbContext {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) {
        }

        DbSet<Attribute> Attributes { get; set; }
        DbSet<Page> Pages { get; set; }

    }
}