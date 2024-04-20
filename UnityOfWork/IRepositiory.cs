using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTasker.UnityOfWork
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        void Create(T item);
        void Update(T item);
        void Delete(object id);
    }
}
