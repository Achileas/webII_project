using System.ComponentModel.DataAnnotations;

namespace webapi_project.Models;
public class SignIn
{
    public string username { get; set; } = null!;
    public string password { get; set; } = null!;
}