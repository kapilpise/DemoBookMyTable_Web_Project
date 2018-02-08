using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BookMyTableEntities.Entities
{


    public class OrderDetails
    {

        public int CustomerID { get; set; }
        public string UserType { get; set; }
        public List<OrderItemDetails> OrderItemDetails { get; set; }
        public int OrderID { get; set; }
        public int HotelId { get; set; }
        public int DishId { get; set; }
        public int IsApproveStatus { get; set; }
        public string ApprovalName { get; set; }
        public decimal TotalAmount { get; set; }
        public int TableID { get; set; }
        public int TotalQuantity { get; set; }
        public string EmailID { get; set; }
        public string CustomerName { get; set; }

    }

    public class OrderItemDetails
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int CustomerID { get; set; }
        public int HotelId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public decimal DishUnitPrice { get; set; }
        public decimal DishTotalAmount { get; set; }
        public int IsVeg { get; set; }
        public string imagePath { get; set; }
        public decimal DishAmount { get; set; }

    }
}