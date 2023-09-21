using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SpecializationList:List<Specialization>
    {
        public SpecializationList() { }
        public SpecializationList(IEnumerable<Specialization> list) : base(list) { }
        public SpecializationList(IEnumerable<BaseEntity> list) : base(list.Cast<Specialization>().ToList()) { }
    }
}
