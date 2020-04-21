//Aliaksandra Hrechka
//CIS237
//04/21/2020
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


        public JsonResult CheckEmail(string emailAddress, int customerID, [FromServices] IRepository<Customer> data)
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
