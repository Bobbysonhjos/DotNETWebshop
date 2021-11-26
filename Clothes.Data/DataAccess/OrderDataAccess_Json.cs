using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Clothes.Data.DataAccess
{
    public class OrderDataAccess_Json : IOrderDataAccess
    {
        public void Add(Order order)
        {
            var list = Read();// hämtar in listan
            list.Add(order);
            Write(list);//  Sparar listan i textfilen
        }

        public List<Order> GetAll()
        {
            return Read();
        }

        public Order GetById(int id)
        {
            var list = Read();
            return list.FirstOrDefault(x => x.Id == id);
        }

        public int GetNewId()
        {
            var list = Read();// Hämtar listan
            if (list.Count < 1)// Kollar om listan innehåller nåt
            {
                return 1; // Om den är tom, Returnar den 1
            }
            return list.Max(x => x.Id) + 1; // Annars returnar den högsta id + 1
        }
        public void UpdateOrder(Order order)
        {
            var list = Read();
            var orderToRemove = list.FirstOrDefault(x => x.Id == order.Id);
            if (orderToRemove is not null)
            {
                list.Remove(orderToRemove);
            }
            list.Add(order);
            Write(list);
        }

        public List<Order> Read()
        {
            if (File.Exists(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Orders.json"))
            {
                var jsonresponse = File.ReadAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Orders.json");
                var list = JsonConvert.DeserializeObject<List<Order>>(jsonresponse);
                if (list is not null)
                {
                    return list;
                }
            }

            return new List<Order>();// metod för att läsa Jsonfil
        }
        private void Write(List<Order> order)
        {
            var jsonstring = JsonConvert.SerializeObject(order);
            File.WriteAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Orders.json", jsonstring);
        }
    }
}
