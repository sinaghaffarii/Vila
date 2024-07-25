using Microsoft.EntityFrameworkCore;

namespace Vila.WebApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Model.Vila> Vilas { get; set; }

    }

}
