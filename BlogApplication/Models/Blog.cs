using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace BlogApplication.Models;

public class Blog
{
    [Key]
    public int ID { get; set; }
    public string BlogId { get; set; }
    public string Title { get; set; }
    public string OwnerId { get; set; }
    
    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}