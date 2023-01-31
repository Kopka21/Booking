using Booking.Data;
using Booking.Models;
using Booking.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Controllers
{
    [Authorize(Policy = "UserAdmin")]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationController(ApplicationDbContext dbContext , UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private async Task<IdentityUser> GetUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return user;
        }
       
        public async Task<IActionResult> GetReservations()
        {
            var user =  await GetUser();

            var tables = await _dbContext.Tables
                .Include(r=>r.Restaurant)
                .Include(r=>r.Reservation)
                .Where(r=>r.Reservation.UserId == user.Id)
                .ToListAsync();

            return View(tables);
        }
    }
}
