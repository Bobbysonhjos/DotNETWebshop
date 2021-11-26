using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Clothes.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly ICartDataAccess cartDataAccess;
        private readonly IProductDataAccess productDataAccess;

        public ShoppingCartModel(ICartDataAccess cartDataAccess, IProductDataAccess productDataAccess)
        {
            this.cartDataAccess = cartDataAccess;
            this.productDataAccess = productDataAccess;
        }


        [BindProperty]
        public int Id { get; set; }

        public ShoppingCart ShoppingCart { get; private set; }

        public IActionResult OnGet()
        {
            var id = HttpContext.Session.GetInt32("CustomerId");
            if (id is null) return RedirectToPage("/SelectCustomer");

            ShoppingCart = cartDataAccess.GetById((int)id);
            if (ShoppingCart.Products.Count < 1)
            {
                return RedirectToPage("./Empty");          
            }
            return Page();
        }


        public IActionResult OnPostRemove()
        {


            ShoppingCart = cartDataAccess.GetById((int)HttpContext.Session.GetInt32("CustomerId"));
            var product = productDataAccess.GetByID(Id);
            ShoppingCart.Remove(product);
            cartDataAccess.UpdateCart(ShoppingCart);


            //cartDataAccess.UpdateCart(cartDataAccess.GetById(1).Remove(productDataAccess.GetByID(Id)));


            return RedirectToPage();
        }
        public IActionResult OnPostCheckout()
        {
            return RedirectToPage("/Checkout");

        }



    }
}
