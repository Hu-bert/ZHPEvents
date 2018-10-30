using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZHPEvents.Core;

namespace ZHPEvents
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly Context _context;

        public DashboardController(Context context)
        {
            _context = context;
        }

        // GET: Dashboard


        public IActionResult Index()
        {
            return View();
        }
    }
}
