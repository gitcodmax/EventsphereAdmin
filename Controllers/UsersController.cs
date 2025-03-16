using EventSphereApp.Data;
using EventSphereApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventSphereApp.Controllers
{
  
    public class UsersController : Controller
    {
        private readonly UserManager<UsersModel> _userManager;

        public UsersController(UserManager<UsersModel> userManager)
        {
            _userManager = userManager;
        }

        [Route("Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // ✅ DELETE USER
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "User deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Error deleting user!";
            }

            return RedirectToAction("Index");
        }
    }
}
