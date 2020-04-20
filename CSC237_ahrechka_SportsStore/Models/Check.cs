using CSC237_ahrechka_SportsStore.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public static class Check
    {
        public static string EmailExists(Repository<Customer> data, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = data.Get(new QueryOptions<Customer>
                {
                    Where = c => c.Email.ToLower() == email.ToLower()

                });

                if (customer != null)
                {
                    msg = "Email address already in use.";
                }
            }
            return msg;
        }
    }
}
