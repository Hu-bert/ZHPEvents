using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZHPEvents.Core;
using ZHPEvents.Core.Entities;
using ZHPEvents.Core.Identity;

namespace ZHPEvents
{

    [Authorize(Roles = "Administrator, Editor, Author, EventEditor, EventAuthor")]
    public class EventsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public EventsController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Index

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Event> events;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["StatusParm"] = sortOrder == "Status" ? "status_desc" : "Status";
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
                 events = _context.Event
                    .Where(e => e.IsDeleted != Deleted.Yes)
                    .Where(e => e != null)
                    .Include(e => e.AddingPerson)
                    .Include(e => e.ConfirmingPerson);
            }
            else
            {
                 events = _context.Event
                    .Where(e => e.IsDeleted != Deleted.Yes)
                    .Where(e => e.AddingPersonId == user.Id)
                    .Include(e => e.AddingPerson)
                    .Include(e => e.ConfirmingPerson);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPersonId.Contains(searchString) 
                                       || e.ConfirmingPersonId.Contains(searchString)); 
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    break;
                case "Status":
                    events = events.OrderBy(e => e.Status);
                    break;
                case "status_desc":
                    events = events.OrderByDescending(e => e.Status);
                    break;
                default:
                    ViewData["CurrentSort"] = "";
                    events = events.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        #endregion

        #region Approved

        public async Task<IActionResult> Approved(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Event> events;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["StatusParm"] = sortOrder == "Status" ? "status_desc" : "Status";
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
                events = _context.Event
                   .Where(e => e.IsDeleted != Deleted.Yes)
                   .Where(e => e != null)
                   .Where(e => e.Status == EventStatus.Approved)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }
            else
            {
                events = _context.Event
                   .Where(e => e.IsDeleted != Deleted.Yes)
                   .Where(e => e.Status == EventStatus.Approved)
                   .Where(e => e.AddingPersonId == user.Id)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPersonId.Contains(searchString)
                                       || e.ConfirmingPersonId.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    break;
                case "Status":
                    events = events.OrderBy(e => e.Status);
                    break;
                case "status_desc":
                    events = events.OrderByDescending(e => e.Status);
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        #endregion

        #region Rejected

        public async Task<IActionResult> Rejected(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Event> events;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["StatusParm"] = sortOrder == "Status" ? "status_desc" : "Status";
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
                events = _context.Event
                   .Where(e => e.IsDeleted != Deleted.Yes)
                   .Where(e => e != null)
                   .Where(e => e.Status == EventStatus.Rejected)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }
            else
            {
                events = _context.Event
                   .Where(e => e.IsDeleted != Deleted.Yes)
                   .Where(e => e.Status == EventStatus.Rejected)
                   .Where(e => e.AddingPersonId == user.Id)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPersonId.Contains(searchString)
                                       || e.ConfirmingPersonId.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    break;
                case "Status":
                    events = events.OrderBy(e => e.Status);
                    break;
                case "status_desc":
                    events = events.OrderByDescending(e => e.Status);
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        #endregion

        #region Archived

        public async Task<IActionResult> Archived(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Event> events;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["StatusParm"] = sortOrder == "Status" ? "status_desc" : "Status";
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
                events = _context.Event
                   .Where(e => e.IsDeleted == Deleted.Yes)
                   .Where(e => e != null)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }
            else
            {
                events = _context.Event
                   .Where(e => e.IsDeleted == Deleted.Yes)
                   .Where(e => e.AddingPersonId == user.Id)
                   .Include(e => e.AddingPerson)
                   .Include(e => e.ConfirmingPerson);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPersonId.Contains(searchString)
                                       || e.ConfirmingPersonId.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    break;
                case "Status":
                    events = events.OrderBy(e => e.Status);
                    break;
                case "status_desc":
                    events = events.OrderByDescending(e => e.Status);
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.AddingPerson)
                .Include(e => e.ConfirmingPerson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, bool status)
        {
            var user = await _userManager.GetUserAsync(User);
            var singleEvent = _context.Event
                .Where(e => e.IsDeleted != Deleted.Yes)
                .Include(e => e.AddingPerson)
                .Include(e => e.ConfirmingPerson)
                .FirstOrDefault(e => e.Id == id);

            if (singleEvent == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Administrator") || User.IsInRole("Editor") || User.IsInRole("EventEditor"))
            {
                try
                {
                    singleEvent.Status = (status == true) ? EventStatus.Approved : EventStatus.Rejected;
                    singleEvent.ConfirmingPersonId = user.Id;
                    _context.Update(singleEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(singleEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound();
            }
            return View(singleEvent);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Type,Category,Date,LastDayOfEntries,Description,Place,NumberOfSeats,RegistrationMail,Organizaer,ForWhom")] Event @event)
        {
            var user = await _userManager.GetUserAsync(User);

            @event.AdditionTime = DateTime.Now;
            @event.AddingPersonId = user.Id;
            @event.Status = EventStatus.Rejected;

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);

            if (@event == null || @event.IsDeleted == Deleted.Yes)
            {
                return NotFound();
            }
            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(@event);
            }
            else if (@event.AddingPersonId == user.Id)
            {
                return View(@event);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,Category,Date,LastDayOfEntries,Description,Place,NumberOfSeats,RegistrationMail,Organizaer,ForWhom")] Event eventFromForm)
        {
            var user = await _userManager.GetUserAsync(User);
            var singleEvent = _context.Event
                .Where(e => e.IsDeleted != Deleted.Yes).
                FirstOrDefault(e => e.Id == eventFromForm.Id);

            if (id != eventFromForm.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole("Administrator") || !User.IsInRole("Editor"))
            {
                if (singleEvent.AddingPersonId != user.Id)
                {
                    return NotFound();
                }
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    singleEvent.Title = !string.IsNullOrEmpty(eventFromForm.Title) ? eventFromForm.Title : singleEvent.Title;
                    _context.Update(singleEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventFromForm.Id))
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
            return View(eventFromForm);
        }

        #endregion

        #region Archive

        public async Task<IActionResult> Archive(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Where(e => e.IsDeleted != Deleted.Yes)
                .Include(e => e.AddingPerson)
                .Include(e => e.ConfirmingPerson)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(@event);
            }
            else if (@event.AddingPersonId == user.Id)
            {
                return View(@event);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var @event = await _context.Event.FindAsync(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                @event.IsDeleted = Deleted.Yes;
                _context.Update(@event);
            }
            else if (@event.AddingPersonId == user.Id)
            {
                @event.IsDeleted = Deleted.Yes;
                _context.Update(@event);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Where(e => e.IsDeleted == Deleted.Yes)
                .Include(e => e.AddingPerson)
                .Include(e => e.ConfirmingPerson)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(@event);
            }
            else if (@event.AddingPersonId == user.Id)
            {
                return View(@event);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var @event = await _context.Event.FindAsync(id);

            if (User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                _context.Event.Remove(@event);
            }
            else if (@event.AddingPersonId == user.Id)
            {
                _context.Event.Remove(@event);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
