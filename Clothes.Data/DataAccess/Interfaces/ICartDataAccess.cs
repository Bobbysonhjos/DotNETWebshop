using Clothes.Core.ModelDTO;

namespace Clothes.Data.DataAccess.Interfaces
{
    public interface ICartDataAccess
    {
         // List<ShoppingCart> GetAll(); 
         void RemoveCart(ShoppingCart cart);
        ShoppingCart GetById(int id);
        void UpdateCart(ShoppingCart cart);
    }
}
