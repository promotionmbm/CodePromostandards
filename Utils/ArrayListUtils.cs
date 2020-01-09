using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest.Utils
{
    class ArrayListUtils
    {
        public static List<String> RemoveDuplicates(List<String> originalArray)
        {
            List<String> array = new List<String>();
            foreach(String value in originalArray)
            {
                if (!array.Contains(value))
                {
                    array.Add(value);
                }
            }
            return array;
        }
    }
}
