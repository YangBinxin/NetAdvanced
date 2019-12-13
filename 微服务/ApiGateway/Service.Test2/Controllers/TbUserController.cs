using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Test2.Controllers
{
    public class TbUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Creat()
        {
            return View();
        }
    }
}