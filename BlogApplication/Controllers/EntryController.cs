using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

public class EntryController : Controller
{
    private readonly BlogDbContext _db;

    public EntryController(BlogDbContext db)
    {
        _db = db;
    }

    
    // GET
    public IActionResult Index()
    {
        IEnumerable<Entry> objEntryList = _db.Entries;
        return View(objEntryList);
    }
    
    
    public IActionResult Create()
    {
        Entry entry = new Entry()
        {
            CreatorId = (string)TempData["UserId"],
            DateOfPublication = DateTime.Now,
            BlogId = (int)TempData["blogId"]
        };
        TempData.Keep("UserId");
        TempData.Keep("blogId");
        return View(entry);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Entry obj)
    {
        if (ModelState.IsValid)
        {
            _db.Entries.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Entry created successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Couldn't create entry. Try again";
        return View(obj);
    }
    
    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var entryFromDb = _db.Entries.Find(id);

        if (entryFromDb == null)
            return NotFound();
        
        return View(entryFromDb);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Entry obj)
    {
        if (ModelState.IsValid)
        {
            _db.Entries.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Entry updated successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot edit entry. Try again";
        return View(obj);
    }
    
    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var entryFromDb = _db.Entries.Find(id);

        if (entryFromDb == null)
            return NotFound();
        
        return View(entryFromDb);
    }
    
    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Entries.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Entries.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Entry deleted successfully";
        return RedirectToAction("Index");
    }
}