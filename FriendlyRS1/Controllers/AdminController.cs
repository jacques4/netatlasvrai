/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyRS1.Repository;
using FriendlyRS1.SignalRChat.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FriendlyRS1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ApplicationDbContext dbnames;
        public AdminController(ApplicationDbContext db)
        {
           dbnames = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Index(Article model)
        //{
        //    await _notificationHubContext.Clients.All.SendAsync("sendToUser", model.articleHeading, model.articleContent);
        //    return View();
        //}
        // public IActionResult Page() => View();
        public async Task<IActionResult> Page()
        {
            return View(await dbnames.Users.ToListAsync());
        }
    }
}
*/




#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.EntityModels;
using FriendlyRS1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
/*using ProjetFormation.Data;
using ProjetFormation.Models;*/

namespace FriendlyRS1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Formations
        public async Task<IActionResult> Index()
        {
            //List<ApplicationUser> uti = new List<ApplicationUser>();
           // if (uti.EmailConfirmed = false)
            //{
              //  return View(await _context.Users.ToListAsync());
            //}
            return View(await _context.Users.ToListAsync());
            /* ApplicationUser uti =new ApplicationUser();
             return (uti.EmailConfirmed = false) ? View(await _context.Users.ToListAsync()) : (IActionResult)View(await _context.Users.ToListAsync());*/
        }

        // GET: Formations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Formations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName")] ApplicationUser formation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formation);
        }

        // GET: Formations/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var formation = await _context.Users.FindAsync(Id);
            if (formation == null)
            {
                return NotFound();
            }
            return View(formation);
        }

        // POST: Formations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateModified,DateCreated,DateOfBirth,ActiveAccount,EmailConfirmed")] ApplicationUser formation)
        */
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmailConfirmed")] ApplicationUser formation)
        {
            if (id != formation.Id)
            {
                return NotFound();
            } 

            if (ModelState.IsValid)
            {
                try
                {
                    var user=_context.Users.Single(e=>e.Id == id);
/*                    var userEntry = _context.Entry(user).Property("EmailConfirmed").CurrentValue = Convert.ToBoolean((string)Request.Form["EmailConfirmed"]);
*/
                    var userEntry = _context.Entry(user).Property("EmailConfirmed").CurrentValue = Convert.ToBoolean((string)Request.Form["1"] );
                    _context.Entry(user).Property("EmailConfirmed").IsModified = true;
                    /*_context.Update(formation);*/

                    await _context.SaveChangesAsync();
                   /* return RedirectToAction(nameof(Index));*/
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.Id))
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
            return View(formation);
        }

        // GET: Formations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Formations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formation = await _context.Users.FindAsync(id);
            _context.Users.Remove(formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
