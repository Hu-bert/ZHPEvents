using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Core;
using ZHPEvents.Core.Entities;
using ZHPEvents.Core.Identity;

namespace ZHPEvents
{
    [Authorize(Roles = "Administrator, Editor, Author, EventEditor, EventAuthor")]
    public class RaportsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public RaportsController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Raports
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Raport> raports;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["AddingPersonSortParm"] = sortOrder == "AddingPerson" ? "addingPerson_desc" : "AddingPerson";
            ViewData["ConfirmingPersonSortParm"] = sortOrder == "ConfirmingPerson" ? "confirmingPerson_desc" : "ConfirmingPerson";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                raports = _context.Raport.Where(e => e != null);
            }
            else
            {
                raports = _context.Raport.Where(e => e.AddingPerson == user.Id);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                raports = raports.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPerson.Contains(searchString) || e.ConfirmingPerson.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    raports = raports.OrderByDescending(e => e.Title);
                    break;
                case "AdditionTime":
                    raports = raports.OrderBy(e => e.AdditionTime);
                    break;
                case "additionTime_desc":
                    raports = raports.OrderByDescending(e => e.AdditionTime);
                    break;
                case "AddingPerson":
                    raports = raports.OrderBy(e => e.AddingPerson);
                    break;
                case "addingPerson_desc":
                    raports = raports.OrderByDescending(e => e.AddingPerson);
                    break;
                case "ConfirmingPerson":
                    raports = raports.OrderBy(e => e.ConfirmingPerson);
                    break;
                case "confirmingPerson_desc":
                    raports = raports.OrderByDescending(e => e.ConfirmingPerson);
                    break;
                default:
                    raports = raports.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Raport>.CreateAsync(raports.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Raports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raport = await _context.Raport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raport == null)
            {
                return NotFound();
            }

            return View(raport);
        }

        // GET: Raports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Raports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Raport @raport)
        {
            var user = await _userManager.GetUserAsync(User);

            @raport.AdditionTime = DateTime.Now;
            @raport.AddingPerson = user.Id;
            @raport.Status = RaportStatus.Rejected;

            if (ModelState.IsValid)
            {
                _context.Add(@raport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@raport);
        }

        // GET: Raports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var raport = await _context.Raport.FindAsync(id);

            if (raport == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(raport);
            }
            else if (raport.AddingPerson == user.Id)
            {
                return View(raport);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Raports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Event raportFromForm)
        {
            var user = await _userManager.GetUserAsync(User);
            var singleRaport = _context.Raport.FirstOrDefault(e => e.Id == raportFromForm.Id);

            if (id != raportFromForm.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole("Administrator") || !User.IsInRole("Editor"))
            {
                if (singleRaport.AddingPerson != user.Id)
                {
                    return NotFound();
                }
            }


            if (ModelState.IsValid)
            {
                try
                {
                    singleRaport.Title = !string.IsNullOrEmpty(raportFromForm.Title) ? raportFromForm.Title : raportFromForm.Title;
                    _context.Update(singleRaport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaportExists(raportFromForm.Id))
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
            return View(raportFromForm);
        }

        // GET: Raports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var raport = await _context.Raport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raport == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(raport);
            }
            else if (raport.AddingPerson == user.Id)
            {
                return View(raport);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Raports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var raport = await _context.Raport.FindAsync(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                _context.Raport.Remove(raport);
            }
            else if (raport.AddingPerson == user.Id)
            {
                _context.Raport.Remove(raport);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


            private bool RaportExists(int id)
        {
            return _context.Raport.Any(e => e.Id == id);
        }
    }
}
