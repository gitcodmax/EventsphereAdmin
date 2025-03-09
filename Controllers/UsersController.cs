using System.Diagnostics;
using EventSphereApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventSphereApp.Controllers
{
    [Route("Users")]
    public class UsersController : Controller
    {
        private readonly string apiUrl = "http://localhost:3000/GetUsers";
        private readonly HttpClient _httpClient;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Invalid username. ";
                return RedirectToAction("Users");
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:3000/DeleteUser?username={username}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "User deleted successfully. ";
            }
            else
            {
                TempData["Error"] = "Failed to delete user. ";
            }

            return RedirectToAction("Users");

        }

        public async Task<ActionResult> Users()
        {

            List<UsersModel> users = new List<UsersModel>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();

                        users = JsonConvert.DeserializeObject<List<UsersModel>>(jsonData);
                    }
                    else
                    {
                        ViewBag.Error = "API returned an error: " + response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Exception: " + ex.Message;
            }

            return View("Users", users);
        }
    }
}
