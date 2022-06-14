using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Helpers.UtilityServices.FilterResults
{
    public static class FilterResults
    {
        public static IEnumerable<T> ApplyFilters<T>(this IEnumerable<T> source,IEnumerable<IFilterService<T>> filters)
        {
            foreach (var item in filters)
            {
                source = item.Execute(source);
            }
            return source;
        }
    }

    public interface IFilterService<T>
    {
        IEnumerable<T> Execute(IEnumerable<T> source);
    }

}
