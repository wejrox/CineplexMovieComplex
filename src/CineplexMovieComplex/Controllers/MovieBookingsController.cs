using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineplexMovieComplex.Models;

namespace CineplexMovieComplex.Controllers
{
    public class MovieBookingsController : Controller
    {
        private readonly wdt_a2_jamesContext _context;

        public MovieBookingsController(wdt_a2_jamesContext context)
        {
            _context = context;    
        }

        // GET: MovieBookings
        public async Task<IActionResult> Index()
        {
            var wdt_a2_jamesContext = _context.MovieBooking.Include(m => m.Cart).Include(m => m.Seat);
            return View(await wdt_a2_jamesContext.ToListAsync());
        }

        // GET: MovieBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieBooking = await _context.MovieBooking.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (movieBooking == null)
            {
                return NotFound();
            }

            return View(movieBooking);
        }

        // GET: MovieBookings/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId");
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId");
            return View();
        }

        // POST: MovieBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,CartId,SeatId")] MovieBooking movieBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", movieBooking.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", movieBooking.SeatId);
            return View(movieBooking);
        }

        // GET: MovieBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieBooking = await _context.MovieBooking.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (movieBooking == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", movieBooking.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", movieBooking.SeatId);
            return View(movieBooking);
        }

        // POST: MovieBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,CartId,SeatId")] MovieBooking movieBooking)
        {
            if (id != movieBooking.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieBookingExists(movieBooking.ReservationId))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", movieBooking.CartId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "SeatId", "SeatId", movieBooking.SeatId);
            return View(movieBooking);
        }

        // GET: MovieBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieBooking = await _context.MovieBooking.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (movieBooking == null)
            {
                return NotFound();
            }

            return View(movieBooking);
        }

        // POST: MovieBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieBooking = await _context.MovieBooking.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.MovieBooking.Remove(movieBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieBookingExists(int id)
        {
            return _context.MovieBooking.Any(e => e.ReservationId == id);
        }
    }
}
