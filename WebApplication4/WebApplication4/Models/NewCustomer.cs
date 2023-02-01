using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class NewCustomer
    {
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int groupCode { get; set; }
        public int factoryCode { get; set; }

    }
}