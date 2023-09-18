using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Entities;

namespace Vertical_Slice_Architecture.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(builder =>
        //builder.OwnsOne(r => r.Tags, tagBuilder => tagBuilder.ToJson()));
        builder.OwnsOne(r => r.Tags));
    }

    public DbSet<Article> Articles { get; set; }
}
