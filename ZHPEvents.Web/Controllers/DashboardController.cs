using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ZHPEvents.Core;
using ZHPEvents.Core.Entities;
using ZHPEvents.Core.Identity;
using ZHPEvents.ViewModels.Dashboard;

namespace ZHPEvents
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dashboard


        public async Task<IActionResult> Index()
        {
            int administrators = 0; int editors = 0; int authors = 0; int eventEditors = 0; int eventAuthors = 0; int raportEditors = 0; int raportAuthors = 0; int users = 0;
            IQueryable<AppUser> appUsers = _context.Users;
            IQueryable<Event> events = _context.Event;
            IQueryable<Raport> raports = _context.Raport;
            var currentUser = await _userManager.GetUserAsync(User);
            foreach (var user in appUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "User"))
                {
                    users++;
                }
                if (await _userManager.IsInRoleAsync(user, "RaportAuthor"))
                {
                    raportAuthors++;
                }
                if (await _userManager.IsInRoleAsync(user, "RaportEditor"))
                {
                    raportEditors++;
                }
                if (await _userManager.IsInRoleAsync(user, "EventAuthor"))
                {
                    eventAuthors++;
                }
                if (await _userManager.IsInRoleAsync(user, "EventEditor"))
                {
                    eventEditors++;
                }
                if (await _userManager.IsInRoleAsync(user, "Author"))
                {
                    authors++;
                }
                if (await _userManager.IsInRoleAsync(user, "Editor"))
                {
                    editors++;
                }
                if (await _userManager.IsInRoleAsync(user, "Administrator"))
                {
                    administrators++;
                }
            }

            IndexViewModel indexView = new IndexViewModel()
            {
                AllUsers = appUsers.Count(),
                Administrators = administrators,
                Editors = editors,
                Authors = authors,
                EventEditors = eventEditors,
                EventAuthors = eventAuthors,
                RaportEditors = raportEditors,
                RaportAuthors = raportAuthors,
                Users = users,

                AdministratorsPercent = ((double)administrators / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                EditorsPercent = ((double)editors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                AuthorsPercent = ((double)authors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                EventEditorsPercent = ((double)eventEditors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                EventAuthorsPercent = ((double)eventAuthors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                RaportEditorsPercent = ((double)raportEditors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                RaportAuthorsPercent = ((double)raportAuthors / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UsersPercent = ((double)users / (double)appUsers.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),

                Events = events.Count(),
                NewEvents = events.Where(e => e.ConfirmingPersonId == null).Count(),
                ApprovedEvents = events.Where(e => e.Status == EventStatus.Approved && e.IsDeleted == Deleted.No).Count(),
                RejectedEvents = events.Where(e => e.Status == EventStatus.Rejected && e.IsDeleted == Deleted.No).Count(),
                ArchivedEvents = events.Where(e => e.IsDeleted == Deleted.Yes).Count(),

                NewEventsPercent = ((double)events.Where(e => e.ConfirmingPersonId == null).Count() / (double)events.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                ApprovedEventsPercent = ((double)events.Where(e => e.Status == EventStatus.Approved && e.IsDeleted == Deleted.No).Count() / (double)events.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                RejectedEventsPercent = ((double)events.Where(e => e.Status == EventStatus.Rejected && e.IsDeleted == Deleted.No).Count() / (double)events.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                ArchivedEventsPercent = ((double)events.Where(e => e.IsDeleted == Deleted.Yes).Count() / (double)events.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),

                Raports = raports.Count(),
                NewRaports = raports.Where(e => e.ConfirmingPerson == null).Count(),
                ApprovedRaports = raports.Where(e => e.ConfirmingPerson == null).Count(),
                RejectedRaports = raports.Where(e => e.ConfirmingPerson == null).Count(),
                ArchivedRaports = raports.Where(e => e.ConfirmingPerson == null).Count(),

                NewRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                ApprovedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                RejectedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                ArchivedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),

                UserEvents = events.Where(e => e.AddingPersonId == currentUser.Id).Count(),
                UserNewEvents = events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.ConfirmingPersonId == null).Count(),
                UserApprovedEvents = events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.Status == EventStatus.Approved && e.IsDeleted == Deleted.No).Count(),
                UserRejectedEvents = events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.Status == EventStatus.Rejected && e.IsDeleted == Deleted.No).Count(),
                UserArchivedEvents = events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.IsDeleted == Deleted.Yes).Count(),

                UserNewEventsPercent = ((double)events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.ConfirmingPersonId == null).Count() / (double)events.Where(e => e.AddingPersonId == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserApprovedEventsPercent = ((double)events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.Status == EventStatus.Approved && e.IsDeleted == Deleted.No).Count() / (double)events.Where(e => e.AddingPersonId == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserRejectedEventsPercent = ((double)events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.Status == EventStatus.Rejected && e.IsDeleted == Deleted.No).Count() / (double)events.Where(e => e.AddingPersonId == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserArchivedEventsPercent = ((double)events.Where(e => e.AddingPersonId == currentUser.Id).Where(e => e.IsDeleted == Deleted.Yes).Count() / (double)events.Where(e => e.AddingPersonId == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),

                UserRaports = raports.Where(e => e.ConfirmingPerson == currentUser.Id).Count(),
                UserNewRaports = raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count(),
                UserApprovedRaports = raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count(),
                UserRejectedRaports = raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count(),
                UserArchivedRaports = raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count(),

                UserNewRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserApprovedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserRejectedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                UserArchivedRaportsPercent = ((double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Where(e => e.ConfirmingPerson == null).Count() / (double)raports.Where(e => e.ConfirmingPerson == currentUser.Id).Count() * 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),

                UserEmailConfirmed = currentUser.EmailConfirmed,
        };
            return View(indexView);
        }
    }
}
