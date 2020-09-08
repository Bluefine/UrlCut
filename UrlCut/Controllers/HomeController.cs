using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UrlCut.Data;
using UrlCut.Interfaces;
using UrlCut.Models;

namespace UrlCut.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlCutContext _context;
        private readonly IShortener _shortener;

        public HomeController(IShortener shortener, UrlCutContext context)
        {
            _shortener = shortener;
            _context = context;
        }

        [HttpGet]
        [Route("/{token}")]
        public IActionResult Index([FromRoute] string token)
        {
            if (string.IsNullOrEmpty(token)) return View();

            var url = _context.URL.ToList().FirstOrDefault(x => x.Token == token);
            if (url == null) return View();

            url.Clicked++;
            _context.SaveChanges();
            return Redirect(url.Link);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(string link)
        {
            if (_shortener.AlreadyExists(link))
                return View("Index", _context.URL.ToList().First(x => x.Link == link).Token);

            var token = _shortener.GenerateUniqueToken();
            var result = _shortener.Add(link, token);
            return result ? View("Index", token) : View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}