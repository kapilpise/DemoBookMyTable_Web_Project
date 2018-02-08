using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyTableEntities.Entities
{
    public class MenuCardEntities
    {

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }

        public string ImagePath { get; set; }
        public string MenuDescription { get; set; }
        public int IsVeg { get; set; }
        public int IsAvailable { get; set; }
        public decimal Price { get; set; }
        public decimal DisCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}