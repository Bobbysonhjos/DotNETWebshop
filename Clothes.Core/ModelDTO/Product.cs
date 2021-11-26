using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Core.ModelDTO
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Quantities { get; set; }



        public decimal GetTotalPrice()
        {
            if (Quantities > 0)
            {
                return Price * Quantities;
            }
            else
            {
                return Price;
            }    
        }
    }
    
}


