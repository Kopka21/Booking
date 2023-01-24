﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public virtual Table Table { get; set; }

        public string UserId { get; set; }

        public virtual List<IdentityUser> Users { get; set; }
    }
}
