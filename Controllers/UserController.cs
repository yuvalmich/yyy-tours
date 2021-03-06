﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using yyytours.Models;

namespace yyytours.Controllers
{
    public class UserController : Controller
    {
        private readonly yyyWebProjContext _context;

        public UserController(yyyWebProjContext context)
        {
            _context = context;
        }

        #region users list
        public async Task<IActionResult> Index(string searchString)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");

            IQueryable<User> users = _context.User;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.FullName.Contains(searchString)
                                       || s.Phone.Contains(searchString)
                                       || s.Email.Contains(searchString));
            }

            return View(await users.ToListAsync());
        }
        #endregion

        #region user details
        public async Task<IActionResult> Details(string email)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");

            var user = await _context.User.FirstOrDefaultAsync(m => m.Email == email);
            if (user == null)
                return View("NotFound");

            return View(user);
        }
        #endregion

        #region create user
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName,Phone,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                if(UserExists(user.Email))
                {
                    ModelState.AddModelError("UserExistsError", "האימייל שבחרת תפוס");
                    return View(user);
                }
                user.Type = UserType.Tourist;
                _context.Add(user);
                await _context.SaveChangesAsync();
                                HttpContext.Session.SetString("Email", user.Email.ToString());
                HttpContext.Session.SetString("FullName", user.FullName.ToString());
                HttpContext.Session.SetInt32("Type", (int)user.Type);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        #endregion

        #region user login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind("Email,Password")] User reqUser)
        {
            var user = _context.User.Where(user => user.Email.Equals(reqUser.Email) && user.Password.Equals(reqUser.Password)).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetString("Email", user.Email.ToString());
                HttpContext.Session.SetString("FullName", user.FullName.ToString());
                HttpContext.Session.SetInt32("Type", (int)user.Type);
                return RedirectToAction("Index", "Home");
            } else
            {
                ModelState.AddModelError("LoginError", "אימייל וסיסמה אינם תואמים");
            }

            return View(reqUser);
        }
        #endregion

        #region user logout
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("Type");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region edit user
        public async Task<IActionResult> Edit(string email)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");

            var user = await _context.User.FindAsync(email);
            if (user == null)
                return View("NotFound");

            ViewData["Type"] = EnumSelect.ToSelectList<UserType>();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string email, [Bind("Email,FullName,Phone,Password,Type")] User user)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");

            if (email != user.Email)
            {
                return View("NotFound");
            }

            bool userTypeNotValid = _context.Trip.Count(t => t.GuideId == email) > 0 && user.Type == UserType.Tourist;

            if (userTypeNotValid)
            {
                ModelState.AddModelError("UserTypeError", "לא ניתן לשנות מדריך עם טיולים לטייל");
            }

            if (ModelState.IsValid && !userTypeNotValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Email))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Type"] = EnumSelect.ToSelectList<UserType>();
            return View(user);
        }
        #endregion

        #region delete user
        public async Task<IActionResult> Delete(string email)
        {
            if (getSessionUserType() != UserType.Admin || HttpContext.Session.GetString("Email").Equals(email))
                return View("NotAuthorized");

            var user = await _context.User.FirstOrDefaultAsync(m => m.Email == email);
            if (user == null)
                return View("NotFound");

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");

            var user = await _context.User.FindAsync(email);

            if (user == null)
                return View("NotFound");

            bool isGuideTours = _context.Trip.Count(t => t.GuideId == email) > 0;
            
            bool isSignToTours = _context.TripRegistration.Count(r => r.UserEmail == email) > 0;

            if (isGuideTours)
            {
                ModelState.AddModelError("userGuideTours", "לא ניתן למחוק מדריך שמדריך טיולים עתידיים");
                return View(user);
            }

            if (isSignToTours)
            {
                ModelState.AddModelError("userSignToTours", "לא ניתן למחוק משתמש שרשום לטיולים");
                return View(user);
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region private methods
        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }

        private UserType? getSessionUserType()
        {
            if (!HttpContext.Session.Keys.Any(k => k.Equals("Type")))
            {
                return null;
            }
            return (UserType)HttpContext.Session.GetInt32("Type");
        }
        #endregion

    }
}
