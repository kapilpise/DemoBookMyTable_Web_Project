using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableEntities.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int CustomerID { get; set; }
        public string UserType { get; set; }
        //public List<OrderItemDetails> OrderItemDetails { get; set; }
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
        public int ispaid { get; set; }
    }
}
