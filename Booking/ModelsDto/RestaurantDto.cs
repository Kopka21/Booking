using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ModelsDto
{
    public class RestaurantDto
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string NumberOfBuilding { get; set; }

        public string PostalCode { get; set; }

        public List<Table> Tables { get; set; }
    }
}
