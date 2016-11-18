using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
    {
        T GetById(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
