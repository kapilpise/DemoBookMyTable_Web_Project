using BookMyTableDataLayer.Data;
using BookMyTableEntities.Entities;
using BookMyTableEntities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace BookMyTableDataLayer.Repository
{
    public class FoodOrderingService :IFoodOrderService
    {
        /// <summary>
        /// get Retaurants details  fro this methods GetllRestaurantDetails
        /// </summary>
        /// <returns> RestaurantEntities </returns>
        public List<RestaurantEntities> GetllRestaurantDetails()
        {           

            List<RestaurantEntities> restaurantEntities = new List<RestaurantEntities>();
            try
            {
                using (bookmytableEntities1 Context = new bookmytableEntities1())
                {

                    var result = Context.pSp_GetRestaurantsDetails(null).ToList();

                    foreach (var item in result)
                    {
                        restaurantEntities.Add(new RestaurantEntities
                        {
                            id = item.id,
                            HotelName = item.HotelName,
                            ContactPersoneName = item.ContactPersoneName,
                            Address = item.Address,
                            City = item.City,
                            Country = item.Country,
                            EmailID = item.EmailID,
                            OpenintHours = item.OpenintHours,
                            PhoneNumber = item.PhoneNumber,
                            Rating = Convert.ToInt32(item.Rating),                           
                            CreateDate = item.CreateDate
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          

            return restaurantEntities;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public List<HotelTableEntities> GetHotelstables(int hotelId)
        {
            List<HotelTableEntities> hotelTableEntities = new List<HotelTableEntities>();

            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                var result = Context.pSp_GetHotelDiningDetails(hotelId).ToList();

                foreach (var item in result)
                {
                    hotelTableEntities.Add(new HotelTableEntities
                    {
                        Id = item.DineID,
                        ISBooked = Convert.ToBoolean(item.IsBooked),
                        TotalTable = Convert.ToInt32(item.TotalTable),
                        TableCapacity= Convert.ToInt32(item.TableCapacity),
                        CreateDate=Convert.ToDateTime(item.CreateDate)
                    });
                }
            };

            return hotelTableEntities;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public List<MenuCardEntities> GetHotelMenuDetails(int hotelId)
        {
            List<MenuCardEntities> menuCardEntities = new List<MenuCardEntities>();

            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                var result = Context.pSp_GetHotelMEnuDetails(hotelId).ToList();
                foreach (var item in result)
                {
                    menuCardEntities.Add(new MenuCardEntities
                    {
                        MenuID = Convert.ToInt32(item.MenuID),
                        MenuName = item.DishName,
                        MenuDescription = Convert.ToString(item.DishDescription),
                        IsVeg = Convert.ToInt32(item.Isveg),
                        IsAvailable = Convert.ToInt32(item.IsAvailable),
                        Price = Convert.ToDecimal(item.Price),
                        ImagePath = item.ImagePath,
                        CreateDate = Convert.ToDateTime(item.CreateDate)
                    });
                }
            };

            return menuCardEntities;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>

        public OrderDetails GetOrderDetails(int OrderID,string emailID)
        {
            OrderDetails orderDetails = new OrderDetails();
            List<OrderItemDetails> orderitemDetails = new List<OrderItemDetails>();

            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                var result = Context.Sp_GetOrderDetails(OrderID, emailID).FirstOrDefault();

                if (result != null)
                {
                      orderDetails.OrderID = result.OrderID;
                      orderDetails.EmailID = result.EmailID;
                      orderDetails.TotalAmount = Convert.ToDecimal(result.TotalAmount);
                      orderDetails.TotalQuantity = Convert.ToInt32(result.TotalQuantity);
                      orderDetails.TableID = Convert.ToInt32(result.TableId);
                      orderDetails.CustomerID = Convert.ToInt32(result.CustomerId);
                      orderDetails.IsApproveStatus = result.IsApproveStatus.Value;
                      var orderItemListDetails = Context.Sp_GetEditOrderDetails(OrderID, result.CustomerId).ToList();
                      for (int i = 0; i < orderItemListDetails.Count; i++)
                      {
                          var orderitem = new OrderItemDetails()
                          {
                              OrderId = orderItemListDetails[i].orderid,
                              TableId = orderItemListDetails[i].tableid.Value,
                              CustomerID = orderItemListDetails[i].customerid,
                              HotelId = orderItemListDetails[i].HotelId.Value,
                              Quantity = orderItemListDetails[i].Quantity.Value,
                              DishId = orderItemListDetails[i].DishId,
                              DishName = orderItemListDetails[i].DishName,
                              DishUnitPrice = orderItemListDetails[i].DishUnitPrice.Value,
                              DishTotalAmount = orderItemListDetails[i].DishTotalAmount.Value,
                              IsVeg = orderItemListDetails[i].IsVeg.Value,
                              imagePath = orderItemListDetails[i].imagePath
                          };
                          orderitemDetails.Add(orderitem);
                      }
                      orderDetails.OrderItemDetails = orderitemDetails;
                }
            }
            return orderDetails;
        }
        

        public OrderDetails GetEditOrderDetails(int OrderID, int CustomerId)
        {
            OrderDetails orderDetail = new OrderDetails();
            
            bookmytableEntities1 Context = new bookmytableEntities1();
            List<OrderItemDetails> orderitemDetails = new List<OrderItemDetails>();

            var userData = (from cus in Context.CustomerTables where cus.Id == CustomerId
                           select new UserVw() { CustomerId = cus.Id, CustomerName = cus.CustomerName, Email = cus.EmailID}).FirstOrDefault();

            orderDetail.EmailID = userData.Email;
            orderDetail.CustomerName = userData.CustomerName == null ? "" : userData.CustomerName;
            orderDetail.IsApproveStatus = Context.salesOrderTables.Where(x => x.OrderID == OrderID).Select(x => x.IsApproveStatus.Value).FirstOrDefault();

            var orderItemListDetails = Context.Sp_GetEditOrderDetails(OrderID, CustomerId).ToList();
            for (int i = 0; i < orderItemListDetails.Count; i++)
            {
                var orderitem = new OrderItemDetails()
                {
                    OrderId = orderItemListDetails[i].orderid,
                    TableId = orderItemListDetails[i].tableid.Value,
                    CustomerID = orderItemListDetails[i].customerid,
                    HotelId = orderItemListDetails[i].HotelId.Value,
                    Quantity = orderItemListDetails[i].Quantity.Value,
                    DishId = orderItemListDetails[i].DishId,
                    DishName = orderItemListDetails[i].DishName,
                    DishDescription = orderItemListDetails[i].DishDescription,
                    DishUnitPrice = orderItemListDetails[i].DishUnitPrice.Value,
                    DishTotalAmount = orderItemListDetails[i].DishTotalAmount.Value,
                    IsVeg = orderItemListDetails[i].IsVeg.Value,
                    imagePath = orderItemListDetails[i].imagePath
                };
                orderitemDetails.Add(orderitem);
            }

            orderDetail.OrderItemDetails = orderitemDetails;
            return orderDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="od"></param>
        /// <returns></returns>
        public List<OrderDetails> AddTablesAndOrderBooking(OrderDetailsEntities od)
        {
            List<OrderDetails> oredrDetails = new List<OrderDetails>();

            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                var result = Context.AddBookMyTableAndMenuOrder(od.HotelId,od.TableID,od.TotalQuantity, od.TotalAmount, od.IsPayed, od.ApprovalName, od.CustomerName, od.PhoneNumber, od.EmailID, od.City, od.Country, od.Address).ToList();

                foreach (var menuItem in od.MenuList)
                {
                    if (menuItem.MenuID != 0)
                    {
                        var result2 = Context.AddsalesOrderItemTable(od.HotelId, menuItem.MenuID, od.TableID, menuItem.ItemTotalAmount, result[0].OrderID, menuItem.Quantity, menuItem.MenuName);
                    }
                }
                foreach (var item in result)
                {
                    oredrDetails.Add(new OrderDetails
                    {
                        CustomerID = Convert.ToInt32(item.CustomerID),
                        OrderID = Convert.ToInt32(item.OrderID),
                        EmailID = od.EmailID
                      //OrderPaymentID = Convert.ToInt32(item.OrderPaymentID),
                    });
                }
                if (!string.IsNullOrEmpty(od.EmailID))
                {
                    string subject = "Your reservation confirmation : " + oredrDetails[0].OrderID;
                    string message = "Your order has been Succesfully placed";
                    //try
                    //{
                    //    SendMailToCustomer(od.EmailID, message, subject);
                    //}
                    //catch (Exception ex)
                    //{
                    //    SqlConnection sqlConnection1 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                    //    SqlCommand cmd = new SqlCommand();
                    //    SqlDataReader reader;
                    //    cmd.CommandText = "INSERT INTO exceptionlogging (ExceptionMsg, InnerExceptionMsg, ExceptionType, ExceptionSource, StackTrace, StatusCode, CreatedDate) VALUES (" + ex.Message + " , " + ex.InnerException != null ? ex.InnerException.Message : "" + " , " + ex.GetType().ToString() + " , " + ex.Source + " , " + ex.StackTrace + " , " + ex.HResult + " , " + DateTime.Now + " )";
                    //    cmd.CommandType = CommandType.Text;
                    //    cmd.Connection = sqlConnection1;
                    //    sqlConnection1.Open();
                    //    reader = cmd.ExecuteReader();
                    //    // Data is accessible through the DataReader object here.
                    //    sqlConnection1.Close();
                    //}
                }

            };

            return oredrDetails;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="employeeID"></param>
        ///// <returns></returns>
        public List<OrderDetails> GetOrderDetailsForApproval(int employeeID)
        {
            List<OrderDetails> OrderDetailsEntities = new List<OrderDetails>();

          
                using (bookmytableEntities1 Context = new bookmytableEntities1())
                {
                    //all orders
                    var result = Context.Sp_GetOrderDetailsToEmployee(employeeID, null).ToList();

                    // iterate on single order
                    foreach (var item in result)
                    {
                        OrderDetails orderdetail = new OrderDetails();
                        List<OrderItemDetails> orderItemDetails = new List<OrderItemDetails>();

                        if (item != null)
                        {
                            orderdetail.OrderID = item.OrderId;
                            orderdetail.TotalAmount = Convert.ToDecimal(item.TotalAmount);
                            //orderdetail.TotalQuantity = Convert.ToInt32(item.);
                            orderdetail.HotelId = item.HotelId.Value;
                            orderdetail.TableID = Convert.ToInt32(item.TableID);
                            orderdetail.IsApproveStatus = item.IsApproveStatus.Value;

                            //getting single order items list.
                            var orderItemListDetails = Context.Sp_GetOrderDetailsForWaiter(item.OrderId).ToList();

                            // iterating on all items of single order
                            for (int i = 0; i < orderItemListDetails.Count; i++)
                            {
                                var orderitem = new OrderItemDetails()
                                {
                                    OrderId = orderItemListDetails[i].orderid,
                                    TableId = orderItemListDetails[i].tableid.Value,
                                    CustomerID = orderItemListDetails[i].customerid,
                                    HotelId = orderItemListDetails[i].HotelId.Value,
                                    Quantity = orderItemListDetails[i].Quantity.Value,
                                    DishId = orderItemListDetails[i].DishId,
                                    DishName = orderItemListDetails[i].DishName,
                                    DishDescription = orderItemListDetails[i].DishDescription,
                                    DishUnitPrice = orderItemListDetails[i].DishUnitPrice.Value,
                                    DishTotalAmount = orderItemListDetails[i].DishTotalAmount.Value,
                                    IsVeg = orderItemListDetails[i].IsVeg.Value,
                                    imagePath = orderItemListDetails[i].imagePath
                                };
                                orderItemDetails.Add(orderitem);  // adding order item to item list // creating list of order items.
                            }
                            orderdetail.OrderItemDetails = orderItemDetails;  // adding order items list to order basic details
                        }
                        OrderDetailsEntities.Add(orderdetail); // finally creating list of orders with there order items inside them

                    }  // iterate on single order ends
                };
            
            return OrderDetailsEntities;
        }


        public List<OrderDetails> GetOrderDetailsForManager(int roleid)
        {
            List<OrderDetails> OrderDetailsEntities = new List<OrderDetails>();


            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                //all orders
                var result = Context.Sp_GetOrderDetailsToEmployee(0, roleid).ToList();

                // iterate on single order
                foreach (var item in result)
                {
                    OrderDetails orderdetail = new OrderDetails();
                    List<OrderItemDetails> orderItemDetails = new List<OrderItemDetails>();

                    if (item != null)
                    {
                        orderdetail.OrderID = item.OrderId;
                        orderdetail.TotalAmount = Convert.ToDecimal(item.TotalAmount);
                        //orderdetail.TotalQuantity = Convert.ToInt32(item.);
                        orderdetail.HotelId = item.HotelId.Value;
                        orderdetail.TableID = Convert.ToInt32(item.TableID);
                        orderdetail.IsApproveStatus = item.IsApproveStatus.Value;

                        //getting single order items list.
                        var orderItemListDetails = Context.Sp_GetOrderDetailsForWaiter(item.OrderId).ToList();

                        // iterating on all items of single order
                        for (int i = 0; i < orderItemListDetails.Count; i++)
                        {
                            var orderitem = new OrderItemDetails()
                            {
                                OrderId = orderItemListDetails[i].orderid,
                                TableId = orderItemListDetails[i].tableid.Value,
                                CustomerID = orderItemListDetails[i].customerid,
                                HotelId = orderItemListDetails[i].HotelId.Value,
                                Quantity = orderItemListDetails[i].Quantity.Value,
                                DishId = orderItemListDetails[i].DishId,
                                DishName = orderItemListDetails[i].DishName,
                                DishDescription = orderItemListDetails[i].DishDescription,
                                DishUnitPrice = orderItemListDetails[i].DishUnitPrice.Value,
                                DishTotalAmount = orderItemListDetails[i].DishTotalAmount.Value,
                                IsVeg = orderItemListDetails[i].IsVeg.Value,
                                imagePath = orderItemListDetails[i].imagePath
                            };
                            orderItemDetails.Add(orderitem);  // adding order item to item list // creating list of order items.
                        }
                        orderdetail.OrderItemDetails = orderItemDetails;  // adding order items list to order basic details
                    }
                    OrderDetailsEntities.Add(orderdetail); // finally creating list of orders with there order items inside them

                }  // iterate on single order ends
            };

            return OrderDetailsEntities;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEmailID"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        public void SendMailToCustomer(string toEmailID,string message,string subject)
        {
         
                string fromAddress = "sdnimitig8@gmail.com";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.smartdatainc.net");// ("smtp.gmail.com");
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toEmailID);
                mail.Subject = subject;
                mail.Body = message;

                SmtpServer.Port = 25; // 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sdnimitig8@gmail.com", "sdn123456");
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);               

          }

        public string UpdateExistingOrder(OrderDetails orderDetail)
        {
            bookmytableEntities1 _dbContext = new bookmytableEntities1();
            var orderItemDetailList = orderDetail.OrderItemDetails;
            var totalCount = 0;
            string message = string.Empty;
            int approvalStatus = orderDetail.UserType == "Customer" ? 0 : orderDetail.UserType == "Waiter" ? 1 : orderDetail.UserType == "Manager" ? 4 : 0;

            foreach (var item in orderDetail.OrderItemDetails)
            {
                if (item.Quantity > 0)
                {
                    try
                    {
                        SqlConnection sqlConnection1 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "UPDATE salesOrderItemTable SET TotalQuantity =" + item.Quantity + " ,TotalAmount=" + item.DishTotalAmount + " ,IsApproveStatus=" + approvalStatus + " WHERE OrderID=" + item.OrderId + " AND HotelId=" + item.HotelId + " AND TableId=" + item.TableId + " AND MenuID=" + item.DishId;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        reader = cmd.ExecuteReader();
                        // Data is accessible through the DataReader object here.
                        sqlConnection1.Close();

                        //var salesOrderItemTable = _dbContext.salesOrderItemTables.Where(x => x.OrderID == item.OrderId && x.HotelId == item.HotelId && x.TableId == item.TableId && x.MenuID == item.DishId).Select(x => x).FirstOrDefault();
                        //salesOrderItemTable.OrderID = item.OrderId;
                        //salesOrderItemTable.MenuID = item.DishId;
                        //salesOrderItemTable.HotelId = item.HotelId;
                        //salesOrderItemTable.IsApproveStatus = 0;
                        //salesOrderItemTable.TotalQuantity = item.Quantity;
                        //salesOrderItemTable.TotalAmount = item.DishTotalAmount;
                        //response = _dbContext.SaveChanges();
                        //if (response > 0)
                        //{
                        //    message = "Success";
                        //}

                        SqlConnection sqlConnection2 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                        SqlCommand cmd1 = new SqlCommand();
                        SqlDataReader reader1;
                        cmd.CommandText = "UPDATE salesOrderTable SET TotalAmount =" + orderDetail.TotalAmount + " ,TableId=" + item.TableId + " ,IsApproveStatus=" + approvalStatus + " ,IsPayed=0" + " WHERE OrderID=" + item.OrderId + " AND HotelId=" + item.HotelId + " AND TableId=" + item.TableId + " AND CustomerId=" + item.CustomerID;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection2;
                        sqlConnection2.Open();
                        reader1 = cmd.ExecuteReader();
                        // Data is accessible through the DataReader object here.
                        sqlConnection2.Close();

                        //var oldSalesOrderTable = _dbContext.salesOrderTables.Where(x => x.HotelId == item.HotelId && x.OrderID == item.OrderId && x.TableId == item.TableId && x.CustomerId == item.CustomerID).Select(x => x).FirstOrDefault();
                        //oldSalesOrderTable.CustomerId = item.CustomerID;
                        //oldSalesOrderTable.IsApproveStatus = 0;
                        //oldSalesOrderTable.TableId = item.TableId;
                        //oldSalesOrderTable.TotalAmount = orderDetail.TotalAmount;
                        //oldSalesOrderTable.IsPayed = 0;
                        //response = _dbContext.SaveChanges();
                        //if (response > 0)
                        //{
                        //    message = "Success";
                        //}
                        message = "Success";
                    }
                    catch (Exception)
                    {
                        message = "Error";
                    }
                }
                if (item.Quantity == 0)
                {
                    try
                    {
                        totalCount++;
                        SqlConnection sqlConnection1 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "UPDATE salesOrderItemTable SET IsDeleted = 1 WHERE OrderID=" + item.OrderId + " AND HotelId=" + item.HotelId + " AND TableId=" + item.TableId + " AND MenuID=" + item.DishId;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        reader = cmd.ExecuteReader();
                        // Data is accessible through the DataReader object here.
                        sqlConnection1.Close();

                        //var salesOrderItemTable = _dbContext.salesOrderItemTables.Where(x => x.OrderID == item.OrderId && x.HotelId == item.HotelId && x.TableId == item.TableId && x.MenuID == item.DishId).Select(x => x).FirstOrDefault();
                        //salesOrderItemTable.IsDeleted = 0;
                        //response = _dbContext.SaveChanges();
                        //if (response > 0)
                        //{
                        //    message = "Success";
                        //}

                        // all items deleted
                        if (orderDetail.OrderItemDetails.Count == totalCount)
                        {
                            SqlConnection sqlConnection2 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                            SqlCommand cmd1 = new SqlCommand();
                            SqlDataReader reader1;
                            cmd.CommandText = "UPDATE salesOrderTable SET IsDeleted = 1 WHERE OrderID=" + item.OrderId + " AND HotelId=" + item.HotelId + " AND TableId=" + item.TableId + " AND CustomerId=" + item.CustomerID;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader1 = cmd.ExecuteReader();
                            // Data is accessible through the DataReader object here.
                            sqlConnection2.Close();
                        }
                       
                        //var oldSalesOrderTable = _dbContext.salesOrderTables.Where(x => x.HotelId == item.HotelId && x.OrderID == item.OrderId && x.TableId == item.TableId && x.CustomerId == item.CustomerID).Select(x => x).FirstOrDefault();
                        //oldSalesOrderTable.IsDeleted = 0;
                        //response = _dbContext.SaveChanges();
                        //if (response > 0)
                        //{
                        //    message = "Success";
                        //}
                        message = "Success";
                    }
                    catch (Exception)
                    {
                        message = "Error";
                    }
                }
            }
            return message;
        }

        public string CancelOrder(int orderId, string UserType)
        {
            bookmytableEntities1 _dbContext = new bookmytableEntities1();
            string message = string.Empty;
            
            int approvalStatus = UserType == "Customer" ? 3 : UserType == "Waiter" ? 2 : UserType == "Manager" ? 5 : 0;
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "UPDATE salesOrderItemTable SET IsDeleted=1 " + ", IsApproveStatus=" + approvalStatus +  " WHERE OrderID=" + orderId;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    reader = cmd.ExecuteReader();
                    sqlConnection1.Close();

                SqlConnection sqlConnection2 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader reader1;
                cmd1.CommandText = "UPDATE salesOrderTable SET IsDeleted=1 " + ", IsApproveStatus=" + approvalStatus + " WHERE OrderID=" + orderId;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader1 = cmd1.ExecuteReader();
                sqlConnection2.Close();

                SqlConnection sqlConnection3 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader reader2;
                cmd2.CommandText = "UPDATE OrderPaymentDetails SET IsDeleted = 1 WHERE CustOrderId=" + orderId;
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = sqlConnection3;
                sqlConnection3.Open();
                reader2 = cmd2.ExecuteReader();
                sqlConnection3.Close();
                
                // Data is accessible through the DataReader object here.
                    message = "Success";
                }
                catch (Exception e)
                {
                    message = "Error";
                }
            return message;
        }

        /// <summary>
        /// Daily Report of Orders paid today
        /// </summary>
        /// <returns></returns>
        public List<DailyReportViewModel> GetDailyReport(DateTime date)
        {
            var beforedate = date.AddDays(-1);
            var afterdate = date.AddDays(1);
            decimal totalacceptedOrderprice=0;
            decimal totalCancelledOrderprice=0;
            List<DailyReportViewModel> TotalOrderofToday = new List<DailyReportViewModel>();
            List<DailyReportAcceptedOrderViewModel> AcceptedOrderReport = new List<DailyReportAcceptedOrderViewModel>();
            List<DailyReportCancelledOrderViewModel> CancelledOrderReport = new List<DailyReportCancelledOrderViewModel>();
            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
                var result = Context.salesOrderTables.Where(x => x.IsDeleted == 0 && x.IsPayed != 0 && x.CreateDate < afterdate && x.CreateDate > beforedate ).ToList();

                foreach (var item in result)
                {
                    var Customerresult = Context.CustomerTables.Where(x => x.IsDeleted != 1 && x.Id == item.CustomerId).FirstOrDefault();
                    
                    AcceptedOrderReport.Add(new DailyReportAcceptedOrderViewModel
                    {
                        Orderid = item.OrderID,
                        OrderAmount = item.TotalAmount,
                        CustomerName = Customerresult.CustomerName,
                        CustomerEmail = Customerresult.EmailID
                    });
                    totalacceptedOrderprice = totalacceptedOrderprice + Convert.ToDecimal(item.TotalAmount);
                }

                var result1 = Context.salesOrderTables.Where(x => x.IsDeleted == 1 && x.IsPayed == 0 && x.CreateDate < afterdate && x.CreateDate > beforedate).ToList();

                foreach (var item in result1)
                {
                    var Customerresult = Context.CustomerTables.Where(x => x.IsDeleted != 1 && x.Id == item.CustomerId).FirstOrDefault();

                    CancelledOrderReport.Add(new DailyReportCancelledOrderViewModel
                    {
                        Orderid = item.OrderID,
                        OrderAmount = item.TotalAmount,
                        CustomerName = Customerresult.CustomerName,
                        CustomerEmail = Customerresult.EmailID
                    });
                    totalCancelledOrderprice = totalCancelledOrderprice + Convert.ToDecimal(item.TotalAmount);
                }
            };
            TotalOrderofToday.Add(new DailyReportViewModel
            {
                AcceptedOrderStatus = AcceptedOrderReport,
                CancelledOrderStatus = CancelledOrderReport,
                TotalCancelledOrderPrice = totalCancelledOrderprice,
                TotalAcceptedOrderPrice = totalacceptedOrderprice
            });

            return TotalOrderofToday;

        }

        public string AuthenticateUser(string username, string password)
        {

            try
            {
                using (bookmytableEntities1 Context = new bookmytableEntities1())
                {
                    //var result = Context.Users.Where(x => x.Username == username && x.Password == password);
                    var query = (from u in Context.Users
                                 join ur in Context.UserRoles on u.Id equals ur.UserId
                                 where u.Username == username && u.Password == password
                                 select new UserRoleViewModel
                                 {
                                     RoleDesc = ur.RoleDesc
                                 }).ToList();

                    return query[0].RoleDesc.ToString() ;
                };
            }
            catch (Exception ex)
            {
                return "no role";
            }
        }

        public string CompletedOrder(long Orderid, long customerid)
        {

            List<salesOrderTableViewModel> tablelistandhotel = new List<salesOrderTableViewModel>(); 
            using (bookmytableEntities1 Context = new bookmytableEntities1())
            {
               var  tableandhotel = Context.salesOrderTables.Where(x => x.CustomerId == customerid && x.OrderID == Orderid).Select(p=>p).ToList();

                foreach(var itemssin in tableandhotel)
                {
                    tablelistandhotel.Add(new salesOrderTableViewModel
                    {
                        HotelId= itemssin.HotelId,
                        TableId=itemssin.TableId


                    });

                }
                                                            
            }
            var message = "";
                    if (Orderid > 0  && customerid > 0)
                    {
                        try
                        {
                            SqlConnection sqlConnection1 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                            SqlCommand cmd = new SqlCommand();
                            SqlDataReader reader;
                            cmd.CommandText = "UPDATE salesOrderTable SET IsPayed =" + 1 + " WHERE OrderID=" + Orderid + " AND CustomerId=" + customerid ;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection1;
                            sqlConnection1.Open();
                            reader = cmd.ExecuteReader();
                            sqlConnection1.Close();

                            SqlConnection sqlConnection2 = new SqlConnection("Data Source=108.168.203.227,7007;Initial Catalog=BookMyTable;User ID=bookmytable;Password=bookmytable");
                            SqlCommand cmd1 = new SqlCommand();
                            SqlDataReader reader1;
                            cmd.CommandText = "UPDATE HotelDiningDetails SET IsBooked =" + 0 + " WHERE Id=" + tablelistandhotel[0].TableId + " AND HotelId=" + tablelistandhotel[0].HotelId;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader1 = cmd.ExecuteReader();
                            message = "Success";
                            return message;
                                }
                                catch (Exception)
                                {
                                    message = "Error";
                                }
                    }
            return "paid unsuccess";
        }

    }
}