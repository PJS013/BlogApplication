namespace BlogApplication.Models;

public class EntryBlogData
{
    public Blog blog;
    public IEnumerable<Entry> entry;

    public EntryBlogData()
    {
        this.blog = new Blog();
        this.entry = new List<Entry>();
    }
}