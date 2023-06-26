using System.ComponentModel.DataAnnotations;

namespace webapi_project.Models;
public class SignUp
{
    public string email { get; set; } = null!;
    public string username { get; set; } = null!;
    public string password { get; set; } = null!;
}