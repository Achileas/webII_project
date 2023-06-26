using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_project.Models;

public class Note
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    [DataType("nvarchar(250)")]
    [ForeignKey("AspNetUsers")]
    public uint ByteLength { get; set; } = 0;
    public uint WordCount { get; set; } = 0;

    public string AuthorId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    // public virtual ApplicationUser Author { get; set; } = null!;
}