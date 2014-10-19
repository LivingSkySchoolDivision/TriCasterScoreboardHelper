using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TricasterHelper
{
    public static class Helpers
    {
        /// <summary>
        /// A more convenient wrapper for Int.TryParse
        /// </summary>
        /// <param name="intString"></param>
        /// <returns></returns>
        public static int ParseInt(string intString)
        {
            int parsedValue = 0;
            if (int.TryParse(intString, out parsedValue))
            {
                return parsedValue;
            }

            return 0;
        }

    }
}
