using Microsoft.EntityFrameworkCore;
using Starbuck.Domain;

namespace Starbuck.Persistence;
internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Coffe> Coffes { get; set; }
    public required DbSet<Ingredient> Ingredients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Category>()
            .HasMany(c => c.Coffes)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Coffe>()
            .Property(p => p.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Coffe>()
            .HasMany(c => c.Ingredients)
            .WithMany(i => i.Coffes)
            .UsingEntity<CoffeIngredient>(
                j => j
                    .HasOne(ci => ci.Ingredient)
                    .WithMany(i => i.CoffeIngredients)
                    .HasForeignKey(ci => ci.IngredientId),
                j => j
                    .HasOne(ci => ci.Coffe)
                    .WithMany(c => c.CoffeIngredients)
                    .HasForeignKey(ci => ci.CoffeId),
                j =>
                {
                    j.HasKey(t => new { t.CoffeId, t.IngredientId });
                }
            );

        modelBuilder.Entity<Category>().HasData(GetCategories());
    }

    private IEnumerable<Category> GetCategories()
    {
        return Enum.GetValues<CategoryEnum>().Select(p => Category.Create((int)p));
    }
}
