using Clothes.Core.ModelDTO;
using System.Collections.Generic;

namespace Clothes.Data.DataAccess.Interfaces
{
    public interface IOrderDataAccess
    {
        List<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        int GetNewId();
        void UpdateOrder(Order order);
    }
}
