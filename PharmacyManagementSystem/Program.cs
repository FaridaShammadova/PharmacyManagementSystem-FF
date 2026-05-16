using FluentValidation;
using System.Reflection;
using PharmacyManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.Data.SeedData;
using PharmacyManagementSystem.Repositories.Interfaces;
using PharmacyManagementSystem.Services.Implementations;
using PharmacyManagementSystem.Repositories.Implementations;

namespace PharmacyManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";

                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<MedicineRepository>();

            builder.Services.AddScoped<SaleService>();
            builder.Services.AddScoped<MedicineService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<WishlistService>();
            builder.Services.AddScoped<PosService>();
            builder.Services.AddScoped<AlertService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await AppDbInitializer.SeedRolesAndAdminAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Seeding error: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
