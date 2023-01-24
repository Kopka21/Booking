using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string NumberOfBuilding { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public virtual List<Table> Tables { get; set; }
    }
}
