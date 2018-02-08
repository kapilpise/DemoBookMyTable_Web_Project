using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableEntities.ViewModels
{
   public class salesOrderTableViewModel
    {

        public int OrderID { get; set; }

        public Nullable<int> HotelId { get; set; }

        public Nullable<int> TableId { get; set; }

        public Nullable<int> TotalQuantity { get; set; }

        public Nullable<int> IsApproveStatus { get; set; }

        public string ApprovalName { get; set; }

        public Nullable<decimal> TotalAmount { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public Nullable<int> CustomerId { get; set; }

        public Nullable<int> IsPayed { get; set; }

        public Nullable<int> IsDeleted { get; set; }
        

    }
}
