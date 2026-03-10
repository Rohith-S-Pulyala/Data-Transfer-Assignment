using DataTransfer_WebApp_Pulyala.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataTransfer_WebApp_Pulyala.Controllers
{
    public class HomeController : Controller
    {
        // Data list methods
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
                    { Name = parts[0].Trim(), 
                      Game = parts[1].Trim(),
                      Sport = sportCat[0].Trim(),
                      Category = sportCat[1].Trim()
                    });
                }
            }

            return countries;
        }

        private string GetCountryCode(string countryName) 
        {
            var codes = new Dictionary<string, string> 
            {
                { "Canada", "ca" },
                { "Sweden", "se" },
                { "Great Britain", "gb" },
                { "Jamaica", "jm" },
                { "Italy", "it" },
                { "Japan", "jp" },
                { "Germany", "de" },
                { "China", "cn" },
                { "Mexico", "mx" },
                { "Brazil", "br" },
                { "Netherlands", "nl" },
                { "USA", "us" },
                { "Thailand", "th" },
                { "Uruguay", "uy" },
                { "Ukraine", "ua" },
                { "Austria", "at" },
                { "Pakistan", "pk" },
                { "Zimbabwe", "zw" },
                { "France", "fr" },
                { "Cyprus", "cy" },
                { "Russia", "ru" },
                { "Finland", "fi" },
                { "Slovakia", "sk" },
                { "Portugal", "pt" }

            };

            // If the name is in the list, the method return the code, else it returns a placeholder.
            return codes.ContainsKey(countryName) ? codes[countryName] : "un";
        }

        public IActionResult Index(string game = "All", string category = "All")
        {
            ViewBag.Game = game;
            ViewBag.Category = category;

            var countries = GetCountryData();

            // Filter logic to order content based on Game(Winter Olympics, Summer Olympics, Paralympics, Youth Olympic Games)
            if (game != "All")
                countries = countries.Where(c => c.Game == game).ToList();

            // Filter logic to order content based on Category(Indoor or Outdoor)
            if (category != "All")
                countries = countries.Where(c => c.Category == category).ToList();

            return View(countries.OrderBy(c => c.Name).ToList()); // Alphabetically sorting countries in data table.
        }

        public IActionResult Details(string id) 
        {
            // GetCountryData Method in use to search for country.
            List<Country> allCountries = GetCountryData();

            // Looks for specific country
            var country = allCountries.FirstOrDefault(c => c.Name == id);

            if (country == null) 
            {
                return NotFound();
            }

            return View(country); // Sends the single country object to the Details view.
        }
    }
}
