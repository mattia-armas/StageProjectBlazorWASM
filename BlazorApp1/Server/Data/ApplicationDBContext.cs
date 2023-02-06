using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlazorApp1.Shared;


namespace BlazorApp1.Server.Data;
public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {}
    public DbSet<Item> Items { get; set; }
}