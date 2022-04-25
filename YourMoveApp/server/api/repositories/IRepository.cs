using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.server.api
{
    internal interface IRepository<T>
    {
        public String Save(T item);

        public T UpdateOrThrowException(String id, T item);

        public void Delete(String id);

        public T FindOrThrowException(String id);

        public List<T> FindMultiple(List<String> ids);
    }
}
