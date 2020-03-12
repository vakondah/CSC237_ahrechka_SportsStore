using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public interface IProductRepository
    {
        // all operations we want to perform
        IEnumerable<Product> GetProducts { get; }

        Product GetProductById(int productId);
    }
}
