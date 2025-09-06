using AbySalto.Junior.Models;
using AbySalto.Junior.Primitives;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<Ordered_Product> ordered_products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<Order>().OwnsOne(order => order.delivery_address);
            modelBuilder.Entity<Order>().HasMany(order => order.products).WithOne(product => product.Order).HasForeignKey(product => product.order_ID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().Property(order => order.state).HasConversion<string>();
            modelBuilder.Entity<Order>().Property(order => order.payment_type).HasConversion<string>();
            modelBuilder.Entity<Order>().Property(order => order.time).HasConversion(value => value.ToTimeSpan(), value => TimeOnly.FromTimeSpan(value));
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
