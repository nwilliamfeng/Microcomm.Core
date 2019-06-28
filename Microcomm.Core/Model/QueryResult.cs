using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm.Model
{
    public sealed class QueryResult<T>
        where T:class
    {

        public int TotalCount { get; set; } = 0;

        public List<T> Items { get; set; } = new List<T>();

       
    }
}
