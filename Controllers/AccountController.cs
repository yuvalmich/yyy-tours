using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using yyy_tours.Models;

namespace yyy_tours.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }

            return View("SignUp", model);
        }
    }
}