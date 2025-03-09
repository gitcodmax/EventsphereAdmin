using System.Net.Http;
using EventSphereApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventSphereApp.Controllers
{
    public class EventsController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "http://localhost:3000/GetEvents";

        public EventsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int eventid)
        {
            if (eventid < 0)
            {
                TempData["Error"] = "Invalid eventid. ";
                return RedirectToAction("Events");
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:3000/DeleteEvent?eventid={eventid}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Event deleted successfully. ";
            }
            else
            {
                TempData["Error"] = "Failed to delete event. ";
            }

            return RedirectToAction("Events");

        }

        [HttpGet]
        public async Task<IActionResult> Events()
        {
            List<EventsModel> events = new List<EventsModel>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    events = JsonConvert.DeserializeObject<List<EventsModel>>(jsonData);
                }
                else
                {
                    ViewBag.Error = "API returned an error: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Exception: " + ex.Message;
            }

            return View(events);
        }
    }
}
