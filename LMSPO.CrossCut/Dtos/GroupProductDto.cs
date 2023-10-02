using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.CrossCut.Dtos
{
    public class GroupProductDto
    {
        public int GroupProductId { get; set; }
        public int GroupId { get; set; }
        public int PurchasedProductId { get; set; }
        public int AddedQuantity { get; set; }
        public GroupDto? Group { get; set; } // Nullable Group navigation property
        public PurchasedProductDto? PurchasedProduct { get; set; } // Nullable PurchasedProduct navigation property
    }
}
