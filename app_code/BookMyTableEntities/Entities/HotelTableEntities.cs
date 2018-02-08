using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyTableEntities.Entities
{
    public class HotelTableEntities
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public int TotalTable { get; set; }
        public int TableCapacity { get; set; }

        public bool ISBooked { get; set; }
        public DateTime CreateDate { get; set; }

    }
}