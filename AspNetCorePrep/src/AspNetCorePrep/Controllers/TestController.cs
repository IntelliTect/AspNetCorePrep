using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePrep.Controllers
{
    public class TestController: Controller
    {
        public IActionResult Index()
        {
            int count = 10;
            return View(count);
        }

        public string Concat(int a, string b = "test")
        {
            return $"{a} is {b}";
        }

    }
}
