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
    public class SelectCustomerModel : PageModel
    {
        private readonly ICustomerDataAccess customerDataAccess;

        public SelectCustomerModel(ICustomerDataAccess customerDataAccess)
        {
            this.customerDataAccess = customerDataAccess;
        }

        public List<Customer> Customers { get; private set; }

        [BindProperty]
        public int Id { get;  set; }

        public IActionResult OnGet()
        {
            Customers = customerDataAccess.GetAll();
            if (Customers.Count < 1) return RedirectToPage("/Index");
            return Page();

        }
        public IActionResult OnPostSelect()
        {
            HttpContext.Session.SetInt32("CustomerId", Id);
            
            return RedirectToPage("/Index");
        }
    }
}
