
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BookMyTableDataLayer.Data
{

using System;
    using System.Collections.Generic;
    
public partial class salesOrderItemTable
{

    public int ID { get; set; }

    public int OrderID { get; set; }

    public int MenuID { get; set; }

    public Nullable<int> HotelId { get; set; }

    public Nullable<int> TableId { get; set; }

    public Nullable<int> TotalQuantity { get; set; }

    public Nullable<int> IsApproveStatus { get; set; }

    public string ApprovalName { get; set; }

    public Nullable<decimal> TotalAmount { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public Nullable<int> IsDeleted { get; set; }



    public virtual salesOrderTable salesOrderTable { get; set; }

}

}