using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Testing
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using CineplexMovieComplex.Controllers;
using CineplexMovieComplex.Data;
using CineplexMovieComplex.Models;
// Cookies
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Web;

namespace CineplexMovieComplex.Tests.Controllers
{
    public class CartsControllerTest
    {
        private wdt_a2_jamesContext _context;
        private CartsController _controller;

        public CartsControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            // Create data storage
            optionsBuilder.UseInMemoryDatabase();
            _context = new wdt_a2_jamesContext(optionsBuilder.Options as DbContextOptions<wdt_a2_jamesContext>);

            // Seed data
            _context.Cart.Add(new Cart());
            MovieTicket m = new MovieTicket();

            _context.SaveChanges();

            // Create test subject
            _controller = new CartsController(_context);
        }

        //FIRST UNIT TEST
        // it invokes the ViewCart action on our controller and validates that the
        // result contains the Cart instance we expect
        // (which was seeded into our in-memory wdt_a2_jamesContext in the test setup constructor).

        [Fact]
        public void View_Cart_1_returns_1()
        {
            // Set up cookie??

        }
    }
}
