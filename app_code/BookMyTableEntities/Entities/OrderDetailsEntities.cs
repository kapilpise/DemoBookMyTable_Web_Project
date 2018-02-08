using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyTableEntities.Entities
{
    public class OrderDetailsEntities
    {
        public int HotelId { get; set; }
        public List<MenuList> MenuList { get; set; }
        public int DishId { get; set; }

        public int TableID { get; set; }

        public int TotalQuantity{ get; set; }
        public decimal TotalAmount { get; set; }

        public string ApprovalName { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailID { get; set; }

        public int IsPayed { get; set; }
    }
    public class MenuList
    {

        public int HotelId { get; set;}

        public int MenuID { get; set; }
        public int Quantity { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public decimal ItemTotalAmount { get; set; }

    }
}
