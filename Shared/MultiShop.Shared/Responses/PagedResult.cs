using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Responses
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public double? LastPrice { get; set; }
        public string? LastId { get; set; }

        public string SortField { get; set; }
        public string SortOrder { get; set; }
    }

}
