using System.Diagnostics.CodeAnalysis;
using BoilerPlate.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Entities
{
    /// <summary>
    /// Nothing special just basic DB context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}