using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Shared.Common
{
    public class PaginationResult<Tentity>
    {
        public PaginationResult(int pageIndex, int pagesize, int totalCount, IEnumerable<Tentity> data)
        {
            PageIndex = pageIndex;
            Pagesize = pagesize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageIndex {  get; set; }
        public int Pagesize {  get; set; }
        public int TotalCount { get; set; } 

        public IEnumerable<Tentity> Data { get; set; }

        public override string ToString()
        {
            return $"Page Index={PageIndex} \n Page Size ={Pagesize} \n Total Count ={TotalCount}\n Data IS {Data}";

        }
    }
}
