
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
    
public partial class OrderPaymentDetail
{

    public int Id { get; set; }

    public Nullable<int> CustOrderId { get; set; }

    public Nullable<int> CustomerId { get; set; }

    public Nullable<int> PaymentTypeID { get; set; }

    public Nullable<decimal> TotalAmount { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public byte[] ts { get; set; }

    public Nullable<int> IsDeleted { get; set; }



    public virtual CustomerTable CustomerTable { get; set; }

}

}
