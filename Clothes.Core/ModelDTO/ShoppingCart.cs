using System.Collections.Generic;
using System.Linq;

namespace Clothes.Core.ModelDTO
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Id { get; set; }
        public string Key { get; set; }



        public void AddProduct(Product product)
        {
            Products.Add(product);
            

        }
        public ShoppingCart Remove(Product product)
        {
            Products.RemoveAll(x => x.Id == product.Id);
            return this;
        }
        public decimal GetTotalCost()
        {
            return Products.Sum(x=>x.Price);
        }
    }   
}
