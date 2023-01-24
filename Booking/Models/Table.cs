using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int NumberOfSeats { get; set; }

        public bool Status { get; set; }

        public int RestaurantId { get; set; }

        public int Number { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
