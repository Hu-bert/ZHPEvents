using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZHPEvents.Core;
using ZHPEvents.Core.Identity;

namespace ZHPEvents
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var user = await _userManager.GetUserAsync(User);
            IQueryable<AppUser> users = _context.Users
                    .Where(u => u.Id != user.Id);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["FristNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fristName_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "lastName_desc" : "LastName";
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
                users = users.Where(u => u.FristName.Contains(searchString)
                                       || u.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "fristName_desc":
                    users = users.OrderByDescending(u => u.FristName);
                    break;
                case "LastName":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "lastName_desc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                default:
                    users = users.OrderBy(u => u.FristName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<AppUser>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Raports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Raports/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Raports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FristName,LastName")] AppUser userFromForm, bool administrator, bool editor, bool author, bool eventEditor, bool eventAuthor, bool raportEditor, bool raportAuthor, bool user)
        {
            if (id != userFromForm.Id)
            {
                return NotFound();
            }
            var shouldRelog = false;
            var userToedit = await _context.Users.FirstOrDefaultAsync(u => u.Id == userFromForm.Id);
            if (ModelState.IsValid)
            {
                try
                {
                    userToedit.FristName = !string.IsNullOrEmpty(userFromForm.FristName) ? userFromForm.FristName : userToedit.FristName;
                    userToedit.LastName = !string.IsNullOrEmpty(userFromForm.LastName) ? userFromForm.LastName : userToedit.LastName;
                    _context.Update(userToedit);
                    await _userManager.UpdateSecurityStampAsync(userFromForm);
                    if (administrator)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "Administrator");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "Administrator");
                        shouldRelog = true;
                    }

                    if (editor)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "Editor");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "Editor");
                        shouldRelog = true;
                    }

                    if (author)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "Author");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "Author");
                        shouldRelog = true;
                    }

                    if (eventEditor)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "EventEditor");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "EventEditor");
                        shouldRelog = true;
                    }

                    if (eventAuthor)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "EventAuthor");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "EventAuthor");
                        shouldRelog = true;
                    }

                    if (raportEditor)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "RaportEditor");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "RaportEditor");
                        shouldRelog = true;
                    }

                    if (raportAuthor)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "RaportAuthor");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "RaportAuthor");
                    }

                    if (user)
                    {
                        await _userManager.AddToRoleAsync(userToedit, "User");
                        shouldRelog = true;
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userToedit, "User");
                        shouldRelog = true;
                    }

                    if (shouldRelog)
                    {
                        await _userManager.UpdateSecurityStampAsync(userToedit);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZHPEventsUserExists(userFromForm.Id))
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

            return View(userFromForm);
        }

        // GET: Raports/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Raports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZHPEventsUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}