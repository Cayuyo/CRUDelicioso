#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CRUDelicioso.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Plato> Platos { get; set; }
}