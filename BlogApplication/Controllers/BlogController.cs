using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

public class BlogController : Controller
{
    
    private readonly BlogDbContext _db;

    public BlogController(BlogDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Blog> objBlogList = _db.Blogs;
        return View(objBlogList);
    }


    public IActionResult GoToEntries(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Blog obj = _db.Blogs.Find(id);
        if (obj.Equals(null))
        {
            return RedirectToAction("Index");
        }
        TempData["blogId"] = id;
        TempData["blogTitle"] = obj.Title;
        TempData["blogBlogId"] = obj.BlogId;
        TempData["blogOwnerId"] = obj.OwnerId;
        return RedirectToAction("Index","Entry");
    }
    
    

    public IActionResult Create()
    {
        Blog blog = new Blog()
        {
            OwnerId = (string)TempData["UserId"]
        };
        TempData.Keep("UserId");

        return View(blog);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Blog obj)
    {
        if (ModelState.IsValid)
        {
            bool state = false;
            IEnumerable<Blog> blogs = _db.Blogs;
            foreach (var blog in blogs)
            {
                if (obj.BlogId.Equals(blog.BlogId))
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                _db.Blogs.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Blog created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Cannot create blog with id same as other. Pick different value";
            }
        }

        return View(obj);
    }
    
    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = _db.Blogs.Find(id);

        if (categoryFromDb == null)
            return NotFound();
        
        return View(categoryFromDb);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Blog obj)
    {
        if (ModelState.IsValid)
        {
            _db.Blogs.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Blog updated successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot edit blog. Try again";
        return View(obj);
    }
    
    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = _db.Blogs.Find(id);

        if (categoryFromDb == null)
            return NotFound();
        
        return View(categoryFromDb);
    }
    
    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Blogs.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Blogs.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Blog deleted successfully";
        return RedirectToAction("Index");
    }
}