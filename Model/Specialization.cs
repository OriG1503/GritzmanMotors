using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Specialization:BaseEntity
    {
        private string _specializationName;

        public string SpecializationName { get => _specializationName; set => _specializationName = value; }
    }
}
