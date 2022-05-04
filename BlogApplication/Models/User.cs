using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models;

public class User
{
    [Key]
    public int ID { get; set; }
    public string userId { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}