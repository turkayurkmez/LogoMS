using Catalog.DataAccess.Data;
using Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccess.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly CatalogDbContext catalogDbContext;

        public EFProductRepository(CatalogDbContext catalogDbContext)
        {
            this.catalogDbContext = catalogDbContext;
        }

        public Product Get(int id)
        {
            return catalogDbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return catalogDbContext.Products.ToList();
        }

        public bool IsExists(int id)
        {
            return catalogDbContext.Products.Any(x => x.Id == id);
        }

        public IEnumerable<Product> Search(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            catalogDbContext.Products.Update(entity);
            catalogDbContext.SaveChanges();
        }
    }
}
