using Catalog.Business.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public interface IProductService
    {
        ProductResponse GetProductById(int id);
        IEnumerable<ProductResponse> GetProducts();
    }
}
