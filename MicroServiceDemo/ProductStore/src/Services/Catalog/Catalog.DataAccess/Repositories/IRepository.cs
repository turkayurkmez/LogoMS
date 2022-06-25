using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccess.Repositories
{
    public interface IRepository<T> where T: IEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll();    
    }
}
