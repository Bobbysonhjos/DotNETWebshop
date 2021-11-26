using Clothes.Core.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Data.DataAccess.Interfaces
{
    public interface IProductDataAccess
    {
        List<Product> GetAll();
        Product GetByID(int id);
    }
}
