﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Data;
using ZHPEvents.Models;

namespace ZHPEvents
{
    public class RaportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Raports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Raport.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Tittle,AdditionTime,AddingPerson,ConfirmingPerson,Status")] Raport raport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raport);
        }

        // GET: Raports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raport = await _context.Raport.FindAsync(id);
            if (raport == null)
            {
                return NotFound();
            }
            return View(raport);
        }

        // POST: Raports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tittle,AdditionTime,AddingPerson,ConfirmingPerson,Status")] Raport raport)
        {
            if (id != raport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaportExists(raport.Id))
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
            return View(raport);
        }

        // GET: Raports/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Raports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raport = await _context.Raport.FindAsync(id);
            _context.Raport.Remove(raport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaportExists(int id)
        {
            return _context.Raport.Any(e => e.Id == id);
        }
    }
}
