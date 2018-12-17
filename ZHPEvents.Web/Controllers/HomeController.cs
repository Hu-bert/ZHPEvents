using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Core;
using ZHPEvents.Core.Entities;
using ZHPEvents.ViewModels.Home;

namespace ZHPEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Event
                .Where(e => e.IsDeleted != Deleted.Yes)
                .Where(e => e.Status == EventStatus.Approved)
                .OrderByDescending(e => e.AdditionTime)
                .Take(3)
                .ToListAsync());
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Events(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var users = _context.Users;

            IQueryable<Event> events = _context.Event
                    .Where(e => e.IsDeleted != Deleted.Yes)
                    .Where(e => e.Status == EventStatus.Approved)
                    .Include(e => e.AddingPerson)
                    .OrderByDescending(e => e.AdditionTime);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AdditionTimeSortParm"] = sortOrder == "AdditionTime" ? "additionTime_desc" : "AdditionTime";
            ViewData["AddingPersonSortParm"] = sortOrder == "AddingPerson" ? "addingPerson_desc" : "AddingPerson";
            ViewData["ConfirmingPersonSortParm"] = sortOrder == "ConfirmingPerson" ? "confirmingPerson_desc" : "ConfirmingPerson";
            ViewData["CollapseShow"] = "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString)
                                       || e.AddingPerson.FristName.Contains(searchString) || e.ConfirmingPerson.FristName.Contains(searchString));
                ViewData["CollapseShow"] = " ";
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "title_desc";
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "AdditionTime";
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "additionTime_desc";
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "AddingPerson";
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "addingPerson_desc";
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "ConfirmingPerson";
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    ViewData["CollapseShow"] = "show";
                    ViewData["Orderby"] = "confirmingPerson_desc";
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    ViewData["Orderby"] = "";
                    ViewData["CollapseShow"] = "show";
                    break;
            }
            int pageSize = 6;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Event(int? id)
        {
            return View(await _context.Event.Include(e => e.AddingPerson).FirstOrDefaultAsync(m => m.Id == id));
        }

    }
}
