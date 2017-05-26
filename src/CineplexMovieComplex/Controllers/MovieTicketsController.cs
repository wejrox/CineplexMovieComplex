using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineplexMovieComplex.Models;
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
        public async Task<IActionResult> ViewCart()
        {
            if (Request.Cookies["S"] == null)
                return Redirect("~/Home/Index");

            var wdt_a2_jamesContext = _context.MovieTicket
                .Include(m => m.Cart)
                .Include(m => m.Seat)
                .Include(m => m.Seat.CineplexMovie)
                .Include(m => m.Seat.CineplexMovie.Movie)
                .Include(m => m.Seat.CineplexMovie.Cineplex);

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
        public async Task<IActionResult> Create(BookSeatFormModel model)
        {
            MovieTicket movieTicket = new MovieTicket();
            if (ModelState.IsValid)
            {
                // Create the movie ticket from the model supplied
                movieTicket = new MovieTicket();
                movieTicket.SeatId = model.SelectedSeatId;

                // Accomodate for seat starting from 1
                //movieTicket.SeatId += 1;
                // Update the seat to be reserved in DB
                Seat s = _context.Seat.Where(se => se.SeatId == movieTicket.SeatId).FirstOrDefault();
                s.Reserved = true;
                _context.Seat.Update(s);

                // Create cart or use current cart if exists
                Cart c;
                bool createNewCart = true;
                if (Request.Cookies["S"] != null)
                {
                    try
                    {
                        movieTicket.CartId = int.Parse(Request.Cookies["S"]);
                        createNewCart = false;
                    }
                    catch (Exception) { /* Tampering has occurred */ }
                }

                // Create new cart for new purchases
                if (createNewCart)
                {
                    c = new Cart();
                    c.LastChange = DateTime.Now;
                    _context.Cart.Add(c);

                    await _context.SaveChangesAsync();

                    movieTicket.CartId = _context.Cart.Where(cart => cart.LastChange == c.LastChange).First().CartId;

                    // Add cookie
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Append("S", c.CartId.ToString(), options);
                }
                // Else update the cart to say it was just used (reset destruction timer)
                else
                {
                    Cart updateCart = _context.Cart.Where(cart => cart.CartId == movieTicket.CartId).First();
                    updateCart.LastChange = DateTime.Now;
                    _context.Cart.Update(updateCart);
                }

                // Process reservation
                _context.MovieTicket.Add(movieTicket);

                // Reduce spaces available
                CineplexMovie cm = _context.CineplexMovie.Where(x => x.CineplexMovieId == s.CineplexMovieId).First();
                cm.SeatsAvailable -= 1;
                _context.Update(cm);

                await _context.SaveChangesAsync();

                return Redirect("~/MovieTickets/ViewCart");
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

        // POST: MovieTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieTicket = await _context.MovieTicket.SingleOrDefaultAsync(m => m.MovieTicketId == id);
            var Seat = await _context.Seat.Where(s => s.SeatId == MovieTicket.SeatId).FirstAsync();
            var CineplexMovie = await _context.CineplexMovie.Where(cm => cm.CineplexMovieId == Seat.CineplexMovieId).FirstAsync();

            if (MovieTicket == null)
            {
                return NotFound();
            }

            // Make seat available again
            Seat.Reserved = false;
            _context.Seat.Update(Seat);
            // Update cineplex movie
            CineplexMovie.SeatsAvailable++;
            _context.CineplexMovie.Update(CineplexMovie);
            // Delete the MovieTicket
            _context.MovieTicket.Remove(MovieTicket);

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart");
        }

        private bool MovieTicketExists(int id)
        {
            return _context.MovieTicket.Any(e => e.MovieTicketId == id);
        }
    }
}
