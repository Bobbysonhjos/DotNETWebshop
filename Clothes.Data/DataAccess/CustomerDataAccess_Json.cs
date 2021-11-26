using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Clothes.Data.DataAccess
{
    public class CustomerDataAccess_Json : ICustomerDataAccess
    {
        public void Add(Customer customer)
        {
            var list = Read();// hämtar in listan
            list.Add(customer);
            Write(list);//  Sparar listan i textfilen
        }

        public List<Customer> GetAll()
        {
             return Read();
        }

        public Customer GetById(int id)
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






        public List<Customer> Read()
        {
            if (File.Exists(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Customers.json"))
            {
                var jsonresponse = File.ReadAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Customers.json");
                var list = JsonConvert.DeserializeObject<List<Customer>>(jsonresponse);
                if (list is not null)
                {
                    return list;
                }
            }

            return new List<Customer>();// metod för att läsa Jsonfil
        }
        private void Write(List<Customer> customers)
        {
            var jsonstring = JsonConvert.SerializeObject(customers);
            File.WriteAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Customers.json", jsonstring);
        }
    }
}
