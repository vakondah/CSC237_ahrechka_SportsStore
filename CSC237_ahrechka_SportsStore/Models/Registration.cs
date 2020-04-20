using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public class Registration
    {
        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }   
}
