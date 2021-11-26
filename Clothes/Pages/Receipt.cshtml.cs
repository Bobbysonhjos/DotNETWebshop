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
    public class ReceiptModel : PageModel
    {
        private readonly IOrderDataAccess orderDataAccess;

        public ReceiptModel(IOrderDataAccess orderDataAccess)
        {
            this.orderDataAccess = orderDataAccess;
        }

        public Order Order { get; private set; }

        public void OnGet(int orderid)
        {
            Order = orderDataAccess.GetById(orderid);
        }
    }
}
