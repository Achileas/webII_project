using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webapi_project.Models;

public class ApplicationUser : IdentityUser
{
    [DataType("nvarchar(MAX)")]
    public string? ProfilePicture { get; set; }

    public ICollection<Note> Notes { get; } = new List<Note>();
}