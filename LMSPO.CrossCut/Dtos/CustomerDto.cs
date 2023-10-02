using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.CrossCut.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSpent { get; set; } // You can calculate and set this value when needed
        public ICollection<PurchasedProductDto> PurchasedProducts { get; set; } = new List<PurchasedProductDto>();
        public ICollection<GroupDto> Groups { get; set; } = new List<GroupDto>();
    }
}
