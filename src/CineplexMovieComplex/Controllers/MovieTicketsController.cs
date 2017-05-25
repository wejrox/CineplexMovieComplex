using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineplexMovieComplex.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CineplexMovieComplex.Controllers
{
    public class MovieTicketsController : Controller
    {
        private readonly wdt_a2_jamesContext _context;

        public MovieTicketsController(wdt_a2_jamesContext context)
        {
            _context = context;    
        }

        // GET: MovieTickets
        public async Task<IActionResult> Index()
        {
            var wdt_a2_jamesContext = _context.MovieTicket.Include(m => m.Cart).Include(m => m.Seat);
            return View(await wdt_a2_jamesContext.ToListAsync());
        }

        // GET: MovieTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieTicket = await _context.MovieTicket.SingleOrDefaultAsync(m => m.MovieTicketId == id);
            if (MovieTicket == null)
            {
                return NotFound();
            }

            return View(MovieTicket);
        }

        // GET: MovieTickets/Create
        public IActionResult Create()
        {
            //ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId");
            //ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId");
            return View();
        }

        // POST: MovieTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieTicket movieTicket)
        {
            if (ModelState.IsValid)
            {
                movieTicket.SeatId += 1;
                Seat s = _context.Seat.Where(se => se.SeatId == movieTicket.SeatId).FirstOrDefault();
                s.Reserved = true;
                _context.Seat.Update(s);
                movieTicket.CartId = 5;
                _context.MovieTicket.Add(movieTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", movieTicket.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", movieTicket.SeatId);
            
            return View(movieTicket);
        }

        // GET: MovieTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieTicket = await _context.MovieTicket.SingleOrDefaultAsync(m => m.MovieTicketId == id);
            if (MovieTicket == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", MovieTicket.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", MovieTicket.SeatId);
            return View(MovieTicket);
        }

        // POST: MovieTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,CartId,SeatId")] MovieTicket MovieTicket)
        {
            if (id != MovieTicket.MovieTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MovieTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTicketExists(MovieTicket.MovieTicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", MovieTicket.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", MovieTicket.SeatId);
            return View(MovieTicket);
        }

        // GET: MovieTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieTicket = await _context.MovieTicket.SingleOrDefaultAsync(m => m.MovieTicketId == id);
            if (MovieTicket == null)
            {
                return NotFound();
            }

            return View(MovieTicket);
        }

        // POST: MovieTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var MovieTicket = await _context.MovieTicket.SingleOrDefaultAsync(m => m.MovieTicketId == id);
            _context.MovieTicket.Remove(MovieTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieTicketExists(int id)
        {
            return _context.MovieTicket.Any(e => e.MovieTicketId == id);
        }
    }
}
