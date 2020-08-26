using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yyytours;
using yyytours.Models;
using Microsoft.AspNetCore.Http;

namespace yyytours.Controllers
{
    public class UserController : Controller
    {
        private readonly yyyWebProjContext _context;

        public UserController(yyyWebProjContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName,Phone,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Type == default(UserType))
                {
                    user.Type = UserType.Tourist;
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind("Email,Password")] User user)
        {
            var obj = _context.User.Where(a => a.Email.Equals(user.Email) && a.Password.Equals(user.Password)).FirstOrDefault();
            if (obj != null)
            {

                HttpContext.Session.SetString("Email", obj.Email.ToString());
                HttpContext.Session.SetString("FullName", obj.FullName.ToString());
                HttpContext.Session.SetString("Type", obj.Type.ToString());
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("LoginError", "אימייל וסיסמה אינם תואמים");
            }

            return View(user);
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("Type");
            return View("../Home/Index");
        }


        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Type"] = EnumSelect.ToSelectList<UserType>();
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string email, [Bind("Email,FullName,Phone,Password,Type")] User user)
        {
            if (email != user.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                        return NotFound();
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

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            var user = await _context.User.FindAsync(email);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }
    }
}
