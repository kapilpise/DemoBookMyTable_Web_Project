
using BookMyTableDataLayer.Data;
using BookMyTableEntities.Entities;
using BookMyTableEntities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableDataLayer.Repository
{
    public interface IFoodOrderService
    {
        List<RestaurantEntities> GetllRestaurantDetails();
        List<MenuCardEntities> GetHotelMenuDetails(int hotelId);
        List<HotelTableEntities> GetHotelstables(int hotelId);
        List<OrderDetails> AddTablesAndOrderBooking(OrderDetailsEntities orderDetails);
        OrderDetails GetOrderDetails(int OrderID,string emailID);
        OrderDetails GetEditOrderDetails(int OrderID, int customerId);
        string UpdateExistingOrder(OrderDetails orderDetail);
        string CancelOrder(int orderId, string UserType);
        List<OrderDetails> GetOrderDetailsForApproval(int employeeID);
        void SendMailToCustomer(string toEmailID, string message, string subject);
        List<OrderDetails> GetOrderDetailsForManager(int roleid);
        List<DailyReportViewModel> GetDailyReport(DateTime date);
        string AuthenticateUser(string username, string password);
        string CompletedOrder(long Orderid, long customerid);

    }
}
