using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreTest.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DotnetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        //public IOptions<Data> OptionsAccessor { get; }
        [ResponseCache(CacheProfileName="Cache1Hour")] 
        public IActionResult Index()
        {
            var now = DateTime.Now;
            return View();
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
    
    
    public class Data
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }
    }
}