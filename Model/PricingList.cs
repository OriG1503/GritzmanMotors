using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PricingList:List<Pricing>
    {
        public PricingList() { }
        public PricingList(IEnumerable<Pricing> list) : base(list) { }
        public PricingList(IEnumerable<BaseEntity> list) : base(list.Cast<Pricing>().ToList()) { }
    }
}
