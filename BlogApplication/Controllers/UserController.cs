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

            TempData["error"] = "Unable to login. Wrong login data provided";
        }

        return View();
    }
    
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel obj)
    {
        if (ModelState.IsValid)
        {
            if(obj.Password == obj.ConfirmPassword)
            {
                bool state = false;
                IEnumerable<User> users = _db.Users;
                foreach (var user in users)
                {
                    if (obj.UserId.Equals(user.userId) &&
                        obj.Email.Equals(user.email))
                    {
                        TempData["error"] =
                            "User with these credentials already exists. Pick different credentials";
                        state = true;
                    }
                }

                if (state == false)
                {
                    User user = new User() {
                        userId = obj.UserId,
                        email = obj.Email,
                        password = obj.Password
                    };
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    TempData["success"] = "Registered successfully";
                    return RedirectToAction("Login");
                }
            }
            else 
            {
                TempData["error"] = "These passwords do not match";
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