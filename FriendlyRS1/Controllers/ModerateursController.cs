


#nullable disable
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.EntityModels;
using FriendlyRS1.Helper;
using FriendlyRS1.Repository;
using FriendlyRS1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FriendlyRS1.Repository.RepostorySetup;
using Microsoft.AspNetCore.Identity;
/*using ProjetFormation.Data;
using ProjetFormation.Models;*/

namespace FriendlyRS1.Controllers
{
    [Authorize(Roles = "Mode")]
    public class ModerateursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public  ModerateursController (ApplicationDbContext context)
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

        public async Task<IActionResult> Warning(int? id)
        {

            var user = _context.Users.Single(e => e.Id == id);
            user.Warning += 1;
            _context.Update(user);
            await _context.SaveChangesAsync();
            /*return View(user);*/
            return RedirectToAction(nameof(Index));

        }



      





        private bool FormationExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
