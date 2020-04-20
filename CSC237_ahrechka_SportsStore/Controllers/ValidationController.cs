using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Controllers
{
    public class ValidationController: Controller
    {
        private Repository<Customer> data { get; set; }
        public ValidationController(SportsProContext ctx) => data = new Repository<Customer>(ctx);

        public JsonResult CheckEmail(string emailAddress, int customerID)
        {
            if (customerID == 0) // only check for new customer, dont check on edit
            {
                string msg = Check.EmailExists(data, emailAddress);
                if (!string.IsNullOrEmpty(msg))
                {
                    return Json(msg);
                }

            }
                TempData["OkEmail"] = true;
                return Json(true);
        }

    }
}
