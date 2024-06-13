using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        //An order belongs to one customer
        //An customer can have many orders
        [ForeignKey("Customer")]
        public int Customer_id { get; set; }
        public virtual Customer Customer { get; set; }
        //Each menu has many orders
        //Each order has one menu
        [ForeignKey("Menu")]
        public int Menu_id { get; set; }
        public virtual Menu Menu { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime Order_Date{ get; set; }
        public int Total_Price { get; set; }
        
    }
}