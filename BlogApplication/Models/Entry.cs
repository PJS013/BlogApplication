using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApplication.Models;

public class Entry
{
    [Key]
    public int EntryId { get; set; }

    public int BlogId { get; set; }
    public string EntryTitle { get; set; }
    public string CreatorId { get; set; }
    public DateTime DateOfPublication { get; set; }
    public string Content { get; set; }
}