using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Notification
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DataType("nvarchar(250)")]
    [ForeignKey("AspNetUsers")]
    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
    public string? Description { get; set; }

    public bool Seen { get; set; } = false;
}