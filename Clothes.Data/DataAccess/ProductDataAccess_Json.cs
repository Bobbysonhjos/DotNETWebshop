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
    public class ProductDataAccess_Json : IProductDataAccess
    {
        public List<Product> GetAll()
        {
            return Read();
        }

        public Product GetByID(int id)
        {
            var list = Read();

            return list.FirstOrDefault(x => x.Id == id); // Gör samma sak som foreach(Linq)


            //foreach (var product in list)
            //{
            //    if (product.Id == id)
            //    {
            //        return product;
            //    }
            //}
            //return null;
        }

        private List<Product> Read()
        {
            var jsonResponse = File.ReadAllText(@"C:\Users\Admin\source\repos\Clothes\Clothes.Data\DataSource\Products.json");
            var list = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);// Returnar en list av products
            return list;
        }
    }
}
