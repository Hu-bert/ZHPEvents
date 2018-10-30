using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Core;
using ZHPEvents.Core.Entities;

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
                .Where(e => e.Status == EventStatus.Approved)
                .OrderByDescending(e => e.AdditionTime)
                .Take(3)
                .ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Events(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Event> events = _context.Event
                    .Where(e => e.Status == EventStatus.Approved)
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
                                       || e.AddingPerson.Contains(searchString) || e.ConfirmingPerson.Contains(searchString));
                ViewData["CollapseShow"] = " ";
            }

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "AdditionTime":
                    events = events.OrderBy(e => e.AdditionTime);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "additionTime_desc":
                    events = events.OrderByDescending(e => e.AdditionTime);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "AddingPerson":
                    events = events.OrderBy(e => e.AddingPerson);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "addingPerson_desc":
                    events = events.OrderByDescending(e => e.AddingPerson);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "ConfirmingPerson":
                    events = events.OrderBy(e => e.ConfirmingPerson);
                    ViewData["CollapseShow"] = "show";
                    break;
                case "confirmingPerson_desc":
                    events = events.OrderByDescending(e => e.ConfirmingPerson);
                    ViewData["CollapseShow"] = "show";
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    break;
            }
            int pageSize = 4;
            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Event(int? id)
        {
            return View(await _context.Event.FirstOrDefaultAsync(m => m.Id == id));
        }

    }
}
