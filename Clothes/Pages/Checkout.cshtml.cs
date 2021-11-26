using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clothes.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICartDataAccess cartDataAccess;
        private readonly ICustomerDataAccess customerDataAccess;
        private readonly IOrderDataAccess orderDataAccess;

        public CheckoutModel(ICartDataAccess cartDataAccess, ICustomerDataAccess customerDataAccess, IOrderDataAccess orderDataAccess)
        {
            this.cartDataAccess = cartDataAccess;
            this.customerDataAccess = customerDataAccess;
            this.orderDataAccess = orderDataAccess;
        }

        public ShoppingCart ShoppingCart { get; private set; }



        [BindProperty]
        [Required]
        public Card Card { get; set; }


        public IActionResult OnGet()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId is null) return RedirectToPage("/SelectCustomer");
            ShoppingCart = cartDataAccess.GetById((int)customerId);
            return Page();
        }
        public IActionResult OnPostPayLater()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId is null) return RedirectToPage("/SelectCustomer");

            var customer = customerDataAccess.GetById((int)customerId);
            ShoppingCart = cartDataAccess.GetById((int)customerId);

            var order = new Order() { IsPaid = false, Products = ShoppingCart.Products, Customer = customer, Id = orderDataAccess.GetNewId(), OrderDate = DateTime.Now }; // Skapar en order 

            orderDataAccess.Add(order);
            ShoppingCart.Products = new List<Product>();
            cartDataAccess.UpdateCart(ShoppingCart);

            return RedirectToPage("./Orders");

        }
        public IActionResult OnPostPay()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId").Value;
            ShoppingCart = cartDataAccess.GetById(customerId);
            if (!ModelState.IsValid) return Page();


            var customer = customerDataAccess.GetById(customerId);
            ShoppingCart = cartDataAccess.GetById(customerId);


            Receipt receipt = new Receipt
            {
                Card = Card,
                PayDate = DateTime.Now
            };

            var order = new Order() { IsPaid = true, Products = ShoppingCart.Products, Customer = customer, Id = orderDataAccess.GetNewId(), OrderDate = DateTime.Now, Receipt = receipt }; // Skapar en order 

            orderDataAccess.Add(order);
            ShoppingCart.Products = new List<Product>();
            cartDataAccess.UpdateCart(ShoppingCart);

            return RedirectToPage("./Orders");
        }

    }

}




