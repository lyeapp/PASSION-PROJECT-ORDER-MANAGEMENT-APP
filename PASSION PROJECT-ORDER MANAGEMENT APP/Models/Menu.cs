using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models
{
    public class Menu
    {
        [Key]
        public int Menu_id { get; set; }
        public string Menu_Name { get; set; }
        public int Menu_Price { get; set; }
    }   
}