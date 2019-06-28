using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm.Model
{
    public class QueryCondition
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;

        public SortMode Sort { get; set; }

        public override int GetHashCode()
        {
            return this.PageIndex.GetHashCode() * 37 + this.PageSize.GetHashCode()*11+this.Sort.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QueryCondition))
                return false;
            var other = obj as QueryCondition;
            if (this.PageSize != other.PageSize)
                return false;
            if (this.PageIndex != other.PageIndex)
                return false;
            return this.Sort == other.Sort;
        }
    }
}
