using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public partial class tblProduct
    {
        public int prod_id { get; set; }

        public string prod_name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
        public decimal prod_price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int prod_qty { get; set; }

        public string prod_pic { get; set; }
    }
}
