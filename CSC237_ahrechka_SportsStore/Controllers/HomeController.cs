using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSC237_ahrechka_SportsStore.Models;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }

    }
}
