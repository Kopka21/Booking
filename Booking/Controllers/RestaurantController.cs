using Booking.Data;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public RestaurantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();

            return View(restaurants);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();

            return View(restaurants);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(x => x.Tables)
                .FirstOrDefaultAsync(r => r.Id == id);

            return View(restaurant);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return RedirectToAction("GetAllAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await GetRestaurant(id);

            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var restaurant = await GetRestaurant(id);

            _dbContext.Restaurants.Remove(restaurant);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("GetAllAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await GetRestaurant(id);

            return View(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> EditPost(Restaurant res)
        {
            var restaurant = await GetRestaurant(res.Id);

            restaurant.Street = res.Street;
            restaurant.NumberOfBuilding = res.NumberOfBuilding;
            restaurant.PostalCode = res.PostalCode;

            _dbContext.Restaurants.Update(restaurant);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("GetAllAdmin");
        }



        private async Task<Restaurant> GetRestaurant(int id)
        {
            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

            return restaurant;
        }
    }
}
