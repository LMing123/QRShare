using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QRServerWeb.Controllers
{
    public class DownLoadController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
