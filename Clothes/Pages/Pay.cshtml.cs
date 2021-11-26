using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothes.Core.ModelDTO;
using Clothes.Data.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clothes.Pages
{
    public class PayModel : PageModel
    {
        private readonly IOrderDataAccess orderDataAccess;

        public PayModel(IOrderDataAccess orderDataAccess)
        {
            this.orderDataAccess = orderDataAccess;
        }


        [BindProperty]
        public Card Card { get ;  set; }

        public Order Order { get; private set; }

        public void OnGet(int orderid)
        {
            Order = orderDataAccess.GetById(orderid);
            HttpContext.Session.SetInt32("OrderId", orderid);
        }

        public IActionResult OnPostPay()
        {
            HttpContext.Session.GetInt32("OrderId");
            Order = orderDataAccess.GetById(HttpContext.Session.GetInt32("OrderId").Value);
            if (!ModelState.IsValid) return Page();

            Receipt receipt = new Receipt
            {
                Card = Card,
                PayDate = DateTime.Now
            };

            Order.Receipt = receipt;
            Order.IsPaid = true;

            orderDataAccess.UpdateOrder(Order);

            return RedirectToPage("./Orders");
        }
    }
}
