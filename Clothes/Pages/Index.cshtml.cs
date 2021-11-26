using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess;
using Clothes.Data.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clothes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductDataAccess productDataAccess;
        private readonly ICartDataAccess cartDataAccess;

        public IndexModel(IProductDataAccess productDataAccess, ICartDataAccess cartDataAccess)
        {
            this.productDataAccess = productDataAccess;
            this.cartDataAccess = cartDataAccess;
        }

        [BindProperty]
        public int Id { get; set; }


        public List<Product> Products { get; private set; }
        public string SortOrder { get; private set; }

        [BindProperty]
        public string Search { get; set; }

        public void OnGet(string sortOrder)
        {
            Products = productDataAccess.GetAll();
            SortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;



            Products = SortOrder switch
            {
                "?sort=HigestPrice" => Products.OrderByDescending(p => p.Price).ToList(),
                "?sort=LowestPrice" => Products.OrderBy(p => p.Price).ToList(),
                _ => Products.OrderBy(p => p.Id).ToList(),
            };
        }

        public IActionResult OnPostSearch()
        {
            Products = productDataAccess.GetAll();
            if (Search is not null)
            {
                var temp = (from prod in Products
                            where prod.Name.ToLower().Contains(Search.ToLower()) || prod.Name.ToLower().StartsWith(Search.ToLower())
                            orderby prod.Name
                            select prod).ToList();

                if (temp.Count > 0)
                {
                    Products = temp;
                }
                else
                {
                    return RedirectToPage();
                }
            }
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            if (!HttpContext.Session.GetInt32("CustomerId").HasValue) return RedirectToPage("/SelectCustomer");
            var id = HttpContext.Session.GetInt32("CustomerId").Value;
            var product = productDataAccess.GetByID(Id);
            var cart = cartDataAccess.GetById(id);
            cart.AddProduct(product);
            cartDataAccess.UpdateCart(cart);



            Products = productDataAccess.GetAll();
            return Page();
        }
    }
}
