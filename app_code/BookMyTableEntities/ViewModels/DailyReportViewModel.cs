using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableEntities.ViewModels
{
    public class DailyReportViewModel
    {
        public List<DailyReportAcceptedOrderViewModel> AcceptedOrderStatus  { get; set; }
        public List<DailyReportCancelledOrderViewModel> CancelledOrderStatus { get; set; }
        public decimal TotalCancelledOrderPrice { get; set; }
        public decimal TotalAcceptedOrderPrice { get; set; }
    }

    public class DailyReportAcceptedOrderViewModel
    {
        public long Orderid { get; set; }
        public decimal? OrderAmount { get; set; }
        public string CustomerName { get; set; }
        public String CustomerEmail { get; set; }
    }

    public class DailyReportCancelledOrderViewModel
    {
        public long Orderid { get; set; }
        public decimal? OrderAmount { get; set; }
        public string CustomerName { get; set; }
        public String CustomerEmail { get; set; }
    }
}
