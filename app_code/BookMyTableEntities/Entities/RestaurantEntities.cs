using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BookMyTableEntities.Entities
{
   
    public class RestaurantEntities
    {

        
        public int id { get; set; }
       
        public string HotelName { get; set; }
       
        public string ContactPersoneName { get; set; }
       
        public string Address { get; set; }
      
        public string City { get; set; }
        
        public string Country { get; set; }
      
        public int Rating { get; set; }
       
        public string PhoneNumber { get; set; }
        
        public string EmailID { get; set; }
       
        public string OpenintHours { get; set; }

        public int TotalTable { get; set; }
        public int TableCapacity { get; set; }

        public int TableAvailable { get; set; }
        public DateTime CreateDate { get; set; }
    }
}