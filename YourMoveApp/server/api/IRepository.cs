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

        public T Update(String id, T item);

        public void Delete(String item);

        public T Find(String id);

        public List<T> FindMultiple(List<String> ids);
    }
}
