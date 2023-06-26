using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using webapi_project.Models;

namespace webapi_project.Data;

public class NoteDbContext : IdentityDbContext<ApplicationUser>
{
    public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) { }
    public DbSet<Note> Note { get; set; } = null!;
    public DbSet<Notification> Notification { get; set; } = default!;
    public DbSet<Share> Share { get; set; } = default!;
    // public DbSet<ApplicationUser> User { get; set; } = default!;
}