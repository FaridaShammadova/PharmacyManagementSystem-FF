using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PharmacyManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PrescriptionRequest> PrescriptionRequests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<StockLog> StockLogs { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cart - User (One to One)
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category - Medicines
            builder.Entity<Category>()
                .HasMany(c => c.Medicines)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Medicine)
                .WithMany(m => m.CartItems)
                .HasForeignKey(ci => ci.MedicineId);

            // Order
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // OrderItem
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Medicine)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(oi => oi.MedicineId);

            // Wishlist
            builder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId);

            // WishlistItem
            builder.Entity<WishlistItem>()
                .HasOne(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistId);

            builder.Entity<WishlistItem>()
                .HasOne(wi => wi.Medicine)
                .WithMany(m => m.WishlistItems)
                .HasForeignKey(wi => wi.MedicineId);

            // Review
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            builder.Entity<Review>()
                .HasOne(r => r.Medicine)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MedicineId);

            // Sale
            builder.Entity<Sale>()
                .HasOne(s => s.Seller)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem
            builder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId);

            builder.Entity<SaleItem>()
                .HasOne(si => si.Medicine)
                .WithMany(m => m.SaleItems)
                .HasForeignKey(si => si.MedicineId);

            // StockLog
            builder.Entity<StockLog>()
                .HasOne(sl => sl.ChangedByUser)
                .WithMany()
                .HasForeignKey(sl => sl.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StockLog>()
                .HasOne(sl => sl.Medicine)
                .WithMany(m => m.StockLogs)
                .HasForeignKey(sl => sl.MedicineId);

            // Notification
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // PrescriptionRequest
            builder.Entity<PrescriptionRequest>()
                .HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PrescriptionRequest>()
                .HasOne(pr => pr.ReviewedBy)
                .WithMany()
                .HasForeignKey(pr => pr.ReviewedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PrescriptionRequest>()
                .HasOne(pr => pr.Order)
                .WithMany()
                .HasForeignKey(pr => pr.OrderId);

            // AuditLog
            builder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision
            builder.Entity<Medicine>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Medicine>()
                .Property(m => m.DiscountPrice)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Sale>()
                .Property(s => s.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<SaleItem>()
                .Property(si => si.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}