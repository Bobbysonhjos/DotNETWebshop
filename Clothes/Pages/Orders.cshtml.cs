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
    public class OrdersModel : PageModel
    {
        private readonly IOrderDataAccess orderDataAccess;

        public OrdersModel(IOrderDataAccess orderDataAccess)
        {
            this.orderDataAccess = orderDataAccess;
        }

        public List<Order> Orders { get; private set; }
        public string PaidSort { get; private set; }

        public IActionResult OnGet(string sortOrder)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId is null) return RedirectToPage("/SelectCustomer");
            Orders = orderDataAccess.GetAll().Where(x => x.Customer.Id == customerId).ToList();

            PaidSort = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;

            Orders = PaidSort switch
            {
                "?sort=IsPaid" => Orders.OrderByDescending(s => s.IsPaid).ToList(),
                "?sort=NotPaid" => Orders.OrderBy(s => s.IsPaid).ToList(),
                _ => Orders.OrderBy(s => s.OrderDate).ToList(),
            };


            return Page();
        }
    }
}
