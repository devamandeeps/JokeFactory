using System.Diagnostics;
using JokeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JokesWebApp.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace JokesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJokeGeneratorService _jokeGeneratorService;
        public HomeController(IJokeGeneratorService jokeGeneratorService, ILogger<HomeController> logger)
        {
            _jokeGeneratorService = jokeGeneratorService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sideMenu = new SideMenuViewModel();            
            var response = _jokeGeneratorService.GetCategories();
            if (response.Result.IsSuccess)
            {
                sideMenu.Categories = response.Result.Result;
            }
            return View(sideMenu);
        }
        public JsonResult GetRandomJoke(bool isRandomNamesOn)
        {
            var response = _jokeGeneratorService.GetRandomJoke().Result;
            if (response.IsSuccess)
            {
                StringBuilder sbJoke = new StringBuilder(response.Result);            
                if (isRandomNamesOn)
                {
                    HandleNameChange(ref sbJoke);
                }
                return Json(sbJoke.ToString());
            }
            else
            {
                return Json(response.ErrorMessage);
            }
        }        

        public JsonResult FindJokeByCategory(string category, bool isRandomNamesOn)
        {
            var response = _jokeGeneratorService.GetJokeByCategory(category).Result;
            
            if (response.IsSuccess)
            {
                StringBuilder sbJoke = new StringBuilder(response.Result);
                if (isRandomNamesOn)
                {
                    if (isRandomNamesOn)
                    {
                        HandleNameChange(ref sbJoke);
                    }
                }
                return Json(sbJoke.ToString());
            }
            else
            {
                return Json(response.ErrorMessage);
            }
        }
        private StringBuilder HandleNameChange(ref StringBuilder sbJoke)
        {
            var responseRandomName = _jokeGeneratorService.GetRandomPerson();
            if (responseRandomName.Result.IsSuccess)
            {
                ReplaceChuckNorrisWithRandomName(sbJoke, $"{ responseRandomName.Result.Result.name + " " + responseRandomName.Result.Result.surname }");
            }
            return sbJoke;
        }
        private static StringBuilder ReplaceChuckNorrisWithRandomName(StringBuilder joke, string randomName)
        {
            string replacedString = Regex.Replace(joke.ToString(), "Chuck Norris", randomName
                                    , RegexOptions.IgnoreCase);
            return joke.Replace(joke.ToString(), replacedString);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
