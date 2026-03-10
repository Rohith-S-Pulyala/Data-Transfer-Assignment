using DataTransfer_WebApp_Pulyala.Extensions;
using DataTransfer_WebApp_Pulyala.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataTransfer_WebApp_Pulyala.Controllers
{
    public class FavoritesController : Controller
    {
        // Same method to store data for countries that participated in Olympics
        private List<Country> GetCountryData()
        {
            string rawData = @"Canada|Winter Olympics|Curling/Indoor
                       Sweden|Winter Olympics|Curling/Indoor
                       Great Britain|Winter Olympics|Curling/Indoor
                       Jamaica|Winter Olympics|Bobsleigh/Outdoor
                       Italy|Winter Olympics|Bobsleigh/Outdoor
                       Japan|Winter Olympics|Bobsleigh/Outdoor
                       Germany|Summer Olympics|Diving/Indoor
                       China|Summer Olympics|Diving/Indoor
                       Mexico|Summer Olympics|Diving/Indoor
                       Brazil|Summer Olympics|Road Cycling/Outdoor
                       Netherlands|Summer Olympics|Cycling/Outdoor
                       USA|Summer Olympics|Road Cycling/Outdoor
                       Thailand|Paralympics|Archery/Indoor
                       Uruguay|Paralympics|Archery/Indoor
                       Ukraine|Paralympics|Archery/Indoor
                       Austria|Paralympics|Canoe Sprint/Outdoor
                       Pakistan|Paralympics|Canoe Sprint/Outdoor
                       Zimbabwe|Paralympics|Canoe Sprint/Outdoor
                       France|Youth Olympic Games|Breakdancing/Indoor
                       Cyprus|Youth Olympic Games|Breakdancing/Indoor
                       Russia|Youth Olympic Games|Breakdancing/Indoor
                       Finland|Youth Olympic Games|Skateboarding/Outdoor
                       Slovakia|Youth Olympic Games|Skateboarding/Outdoor
                       Portugal|Youth Olympic Games|Skateboarding/Outdoor";

            var countries = new List<Country>();

            foreach (var line in rawData.Split('\n'))
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    var sportCat = parts[2].Split('/');
                    countries.Add(new Country
                    {
                        Name = parts[0].Trim(),
                        Game = parts[1].Trim(),
                        Sport = sportCat[0].Trim(),
                        Category = sportCat[1].Trim()
                    });
                }
            }

            return countries;
        }

        [HttpPost]
        public IActionResult Add(string id) 
        {
            // Calls helper method GetCountryData() to get list of all countries.
            var allCountries = GetCountryData();

            // Finds the specific country selected by the user.
            var country = allCountries.FirstOrDefault(c => c.Name == id);

            if (country != null) 
            {
                // Gets favorites from Session
                var favorites = HttpContext.Session.GetObject<List<Country>>("MyFavorites") ?? new List<Country>();

                // Adds if page does not have favorites
                if (!favorites.Any(f => f.Name == country.Name)) 
                {
                    favorites.Add(country);
                    HttpContext.Session.SetObject("MyFavorites", favorites);
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            // Get the list from session to show on Favorites Page
            var favorites = HttpContext.Session.GetObject<List<Country>>("MyFavorites") ?? new List<Country>();
            return View(favorites);
        }

        [HttpPost]
        public IActionResult Clear() 
        {
            HttpContext.Session.Remove("MyFavorites");
            return RedirectToAction("Index", "Home");
        }
    }
}
