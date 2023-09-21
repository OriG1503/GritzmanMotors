using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EmployeeList:List<Employee>
    {
        public EmployeeList() { }
        public EmployeeList(IEnumerable<Employee> list) : base(list) { }
        public EmployeeList(IEnumerable<BaseEntity> list) : base(list.Cast<Employee>().ToList()) { }
    }
}
