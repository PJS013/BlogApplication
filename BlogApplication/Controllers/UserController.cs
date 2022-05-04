using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlogApplication.Controllers;

public class UserController : Controller
{
    
    private readonly UserDbContext _db;

    public UserController(UserDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Login()
    {
        //TempData.Remove("UserId");
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(User obj)
    {
        if (ModelState.IsValid)
        {
            IEnumerable<User> users = _db.Users;
            foreach (var user in users)
            {
                if (obj.userId.Equals(user.userId) &&
                    obj.email.Equals(user.email) &&
                    obj.password.Equals(user.password))
                {
                    TempData["userID"] = user.userId;
                    TempData["success"] = "Logged in successfully";
                    return RedirectToAction("Index", "Blog");
                }
            }

            TempData["loginError"] = "Unable to login. Wrong login data provided";
        }

        return View();
    }
    
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Register(User obj)
    {
        if (ModelState.IsValid)
        {
            bool state = false;
            IEnumerable<User> users = _db.Users;
            foreach (var user in users)
            {
                if (obj.userId.Equals(user.userId) &&
                    obj.email.Equals(user.email))
                {
                    TempData["registerError"] =
                        "User with these credentials already exists. Pick different credentials";
                    state = true;
                    //break;
                }
            }

            if (state == false)
            {
                _db.Users.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Logged in successfully";
                return RedirectToAction("Login");
            }
        }
        return View();
    }
    
    
    
    public IActionResult Logout()
    {
        TempData.Remove("UserId");
        return RedirectToAction("Index", "Home");
    }
}