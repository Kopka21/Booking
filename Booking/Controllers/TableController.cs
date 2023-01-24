using Booking.Data;
using Booking.Models;
using Booking.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.Controllers
{
    [Authorize(Policy = "UserAdmin")]
    public class TableController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public TableController(ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            this.userManager = userManager;
        }

        private async Task<IdentityUser> GetUser()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            return user;
        }

        //user role 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTables(int id)
        {
            var tables = await _dbContext.Tables
                .Where(t => t.RestaurantId == id && t.Status == true)
                .ToListAsync();

            return View(tables);
        }
        //user role
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(r => r.Id == id);

            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> BookPost(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(r => r.Id == id);

            if (table == null)
            {
                return NotFound();
            }
            var user = await GetUser();

            Reservation reservation = new Reservation();

            reservation.TableId = table.Id;

            reservation.UserId = user.Id;

            _dbContext.Add(reservation);
            await _dbContext.SaveChangesAsync();


            table.Status = false;

            _dbContext.Tables.Update(table);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { });
        }

        [Authorize(Policy = "Admin")]
        public IActionResult CreateTable(int id)
        {
            TableDto dto = new();

            dto.RestaurantId = id;

            return View(dto);
        }

        //admin role
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreatePost(TableDto dto)
        {
            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == dto.RestaurantId);

            Table table = new();

            table.RestaurantId = dto.RestaurantId;
            table.Status = true;
            table.NumberOfSeats = dto.NumberOfSeats;
            table.Number = dto.Number;

            _dbContext.Tables.Add(table);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurant", new { id = dto.RestaurantId });
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(r => r.Id == id);

            if (table == null)
            {
                return NotFound();
            }
            table.Status = true;

            var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.TableId == table.Id);

            if (reservation == null)
            {
                return NotFound();
            }

            _dbContext.Update(table);
            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurant", new { id = table.RestaurantId });
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(d => d.Id == id);

            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(d => d.Id == id);

            if (table == null)
            {
                return NotFound();
            }

            int restaurantId = table.RestaurantId;

            _dbContext.Tables.Remove(table);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurant", new { Id = restaurantId });
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(d => d.Id == id);

            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> EditPost(Table tab)
        {
            var table = await _dbContext.Tables.FirstOrDefaultAsync(d => d.Id == tab.Id);


            if (table == null)
            {
                return NotFound();
            }

            int restaurantId = table.RestaurantId;

            table.NumberOfSeats = tab.NumberOfSeats;

            _dbContext.Tables.Update(table);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurant", new { Id = restaurantId });
        }
    }
}
