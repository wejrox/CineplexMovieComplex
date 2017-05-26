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
    public class CineplexMoviesController : Controller
    {
        private readonly wdt_a2_jamesContext _context;

        public CineplexMoviesController(wdt_a2_jamesContext context)
        {
            _context = context;    
        }

        // GET: CineplexMovies
        public async Task<IActionResult> Index()
        {
            var wdt_a2_jamesContext = _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie);
            return View(await wdt_a2_jamesContext.ToListAsync());
        }
        
        // GET: CineplexMovies by Cineplex or Movie Name
        public async Task<IActionResult> Search(string SearchString, string SearchType)
        {
            if (SearchType == "MovieName")
            {
                var namedMovies = from m in _context.CineplexMovie
                                  select m;

                // Check for named movies
                if (!String.IsNullOrEmpty(SearchString))
                    namedMovies = namedMovies.Where(s => s.Movie.Title.Contains(SearchString));
                return View(await namedMovies.Include(m => m.Movie).Include(c => c.Cineplex).ToListAsync());
            }
            else if (SearchType == "CineplexName")
            {
                var cineplexMovies = from m in _context.CineplexMovie
                                     select m;

                // Check for cineplex specified movies
                if (!String.IsNullOrEmpty(SearchString))
                    cineplexMovies = cineplexMovies.Where(s => s.Cineplex.Location.Contains(SearchString));
                return View(await cineplexMovies.Include(m => m.Movie).Include(c => c.Cineplex).ToListAsync());
            }

            var wdt_a2_jamesContext = _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie);
            return View(await wdt_a2_jamesContext.Include(m => m.Movie).Include(c => c.Cineplex).ToListAsync());
        }

        // GET: CineplexMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.Include(m => m.Movie).Include(c => c.Cineplex).SingleOrDefaultAsync(m => m.CineplexMovieId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // GET: Display seating to choose from
        public async Task<IActionResult> SelectSeat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.Include(cm => cm.Seat).Include(cm => cm.Movie).Include(cm => cm.Cineplex).SingleOrDefaultAsync(m => m.CineplexMovieId == id);

            if (cineplexMovie == null)
            {
                return NotFound();
            }

            BookSeatFormModel bookSeatFormModel = new BookSeatFormModel();
            bookSeatFormModel.CineplexMovie = cineplexMovie;

            return View(bookSeatFormModel);
        }

        // GET: CineplexMovies/Create
        public IActionResult Create()
        {
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location");
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription");
            return View();
        }

        // POST: CineplexMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CineplexMovieId,CineplexId,Day,MovieId,SeatsAvailable,ViewingTime")] CineplexMovie cineplexMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cineplexMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexMovieId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CineplexMovieId,CineplexId,Day,MovieId,SeatsAvailable,ViewingTime")] CineplexMovie cineplexMovie)
        {
            if (id != cineplexMovie.CineplexMovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cineplexMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CineplexMovieExists(cineplexMovie.CineplexMovieId))
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
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexMovieId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexMovieId == id);
            _context.CineplexMovie.Remove(cineplexMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CineplexMovieExists(int id)
        {
            return _context.CineplexMovie.Any(e => e.CineplexMovieId == id);
        }
    }
}
