using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Manager:Person
    {
        private Role _roleCode;

        public Role RoleCode { get => _roleCode; set => _roleCode = value; }
    }
}
