using Clothes.Core.ModelDTO;
using System.Collections.Generic;

namespace Clothes.Data.DataAccess.Interfaces
{
    public interface ICustomerDataAccess
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer customer);
        int GetNewId();
    }
}
