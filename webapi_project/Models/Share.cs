using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_project.Models;

public class Share
{
    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("Note")]
    public int NoteId { get; set; }
    [Required]
    [DataType("nvarchar(250)")]
    [ForeignKey("AspNetUsers")]
    public string AuthorId { get; set; } = null!;
    [Required]
    [DataType("nvarchar(250)")]
    [ForeignKey("AspNetUsers")]
    public string SharedUserId { get; set; } = null!;
    public virtual ApplicationUser SharedUser { get; set; } = null!;
}