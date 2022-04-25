using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.util
{
    internal class ObjectUtil
    {
        public static void ValidateIdOrThrowException(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ivalid id ", nameof(id));
            }
        }
        public static void ValidateNotNullOrThrowException(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        public static List<T> CloneList<T>(List<T> list)
        {
            List<T> result = new List<T> ();
            list.ForEach (t => result.Add (t));
            return result;
        }
    }
}
