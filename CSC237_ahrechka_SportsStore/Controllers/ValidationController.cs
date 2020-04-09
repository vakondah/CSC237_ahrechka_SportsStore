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
        private SportsProContext context;
        public ValidationController(SportsProContext ctx) => context = ctx;

        public JsonResult CheckEmail(string emailAddress, int customerID)
        {
            if (customerID == 0) // only check for new customer, dont check on edit
            {
                string msg = Check.EmailExists(context, emailAddress);
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
