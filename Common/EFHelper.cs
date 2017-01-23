using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EFHelper
    {
        public static void UpdateModel<T>(T current, T oldModel) where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var pi in properties)
            {
                var value = pi.GetValue(current);
                if (pi.PropertyType == typeof(DateTime) && ((DateTime)value) == DateTime.MinValue)
                {
                    continue;
                }

                if (value == null)
                    continue;
                pi.SetValue(oldModel, value);
            }
        }
    }
}
