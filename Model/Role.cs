using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Role:BaseEntity
    {

        private string _roleName;

        public string RoleName { get => _roleName; set => _roleName = value; }
    }
}
