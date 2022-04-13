using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.util
{
    internal class ObjectUtil
    {
        public static List<T> CloneList<T>(List<T> list)
        {
            List<T> result = new List<T> ();
            list.ForEach (t => result.Add (t));
            return result;
        }
    }
}
