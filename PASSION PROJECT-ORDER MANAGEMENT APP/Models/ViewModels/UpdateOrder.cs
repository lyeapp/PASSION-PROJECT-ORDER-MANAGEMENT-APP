using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models.ViewModels
{
    public class UpdateOrder
    {
        //This viewmodel is a class which stores information that we need to present to /Order/Update/{}

        //the existing order information

        public OrderDto SelectedOrder { get; set; }

        // all menu to choose from when updating this order

        public IEnumerable<MenuDto> MenuOptions { get; set; }
    }
}