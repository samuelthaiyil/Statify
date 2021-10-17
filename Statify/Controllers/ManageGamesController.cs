using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatTracker.Controllers
{
    public class ManageGamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
