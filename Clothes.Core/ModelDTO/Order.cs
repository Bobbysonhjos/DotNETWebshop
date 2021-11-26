using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Core.ModelDTO
{
   public class Order 
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Key { get; set; }
        public bool IsPaid { get; set; }
        public Customer Customer { get; set; }
        public Receipt Receipt { get; set; }

        public decimal GetTotalCost()
        {
            return Products.Sum(x => x.GetTotalPrice());
        }
    }
   
}
