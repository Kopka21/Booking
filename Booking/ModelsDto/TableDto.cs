using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ModelsDto
{
    public class TableDto
    {
        public int RestaurantId { get; set; }

        public int NumberOfSeats { get; set; }

        public int Number { get; set; }

        public bool Status { get; set; }
    }
}
