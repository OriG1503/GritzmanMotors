using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CarCompanyList:List<CarCompany>
    {
        public CarCompanyList() { }

        public CarCompanyList(IEnumerable<CarCompany> list) : base(list) { }

        public CarCompanyList(IEnumerable<BaseEntity> list) : base(list.Cast<CarCompany>().ToList()) { }   
    }
}
