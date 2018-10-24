﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Data;
using ZHPEvents.Models;
using ZHPEvents.Models.Identity;

namespace ZHPEvents
{

    [Authorize(Roles = "Administrator, Editor, Autho, EventEditor, EventAuthor")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ZHPEventsUser> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<ZHPEventsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Events

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin") || User.IsInRole("Editor") )
            {
                return View(await _context.Event.ToListAsync());
            }
            else
            {
                return View(await _context.Event.Where(e => e.AddingPerson == user.Id).ToListAsync());
            }
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tittle")] Event @event)
        {
            var user = await _userManager.GetUserAsync(User);

            @event.AdditionTime = DateTime.Now;
            @event.AddingPerson = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Admin") || User.IsInRole("Editor"))
            {
                return View(@event);
            }
            else if (@event.AddingPerson == user.Id)
            {
                return View(@event);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tittle")] Event @event)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id != @event.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") || !User.IsInRole("Editor"))
            {
                return NotFound();
            }
            else if (@event.AddingPerson != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Editor"))
            {
                return View(@event);
            }
            else if (@event.AddingPerson == user.Id)
            {
                return View(@event);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var @event = await _context.Event.FindAsync(id);

            if (User.IsInRole("Admin") || User.IsInRole("Editor"))
            {
                _context.Event.Remove(@event);
            }
            else if (@event.AddingPerson == user.Id)
            {
                _context.Event.Remove(@event);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
