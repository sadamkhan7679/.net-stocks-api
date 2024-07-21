using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext: IdentityDbContext<AppUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ): base(options)
    {
        
        
    }
    
    
    // Add Stock Model to the database
    public DbSet<Stock> Stocks { get; set; } = null!;
    
    // Add Comment Model to the database
    public DbSet<Comment> Comments { get; set; } = null!;
    
    
}