using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models
{
    public class Customer
    {
        [Key]
        public int Customer_id { get; set; }
        public string Customer_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Phone_no { get; set; }
        public string Email { get; set; }
        public DateTime Registration_date { get; set; }



    }
}