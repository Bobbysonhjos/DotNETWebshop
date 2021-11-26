using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Data.DataAccess
{
    public class CartDataAccess_Json : ICartDataAccess
    {
        public ShoppingCart GetById(int id)
        {
            var list = Read();
            var cart = list.FirstOrDefault(x => x.Id == id);
            var cartReturn = cart is null ? new ShoppingCart()  {  Id = id} : cart;
            return cartReturn;

        }

        public void RemoveCart(ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public void UpdateCart(ShoppingCart cart)
        {
            var list = Read();
            var carts = list.FirstOrDefault(x => x.Id == cart.Id);
            if (carts is not null)
            {
                list.Remove(carts);
            }
            list.Add(cart);
            Write(list);
        }
        private List<ShoppingCart> Read()
        {
            var jsonresponse = File.ReadAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\ShoppingCarts.json");
            var list = JsonConvert.DeserializeObject<List<ShoppingCart>>(jsonresponse);
            if (list is not null)
            {
                return list;
            }
            return new List<ShoppingCart>();// metod för att läsa Jsonfil
        }
        private void Write(List<ShoppingCart> shoppingCarts)
        {
            var jsonstring = JsonConvert.SerializeObject(shoppingCarts);
            File.WriteAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\ShoppingCarts.json", jsonstring);
        }
    }
}
