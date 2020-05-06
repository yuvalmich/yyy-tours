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
        #region SignUp
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
        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(User model)
        {
            if (IsLoginValid(model.Email, model.Password))
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("", "אימייל או סיסמה שגויים");
            return View("SignIn", model);
        }

        private bool IsLoginValid(string email, string password)
        {
            return false;
        }
        #endregion
    }
}