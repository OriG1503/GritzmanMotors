using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employee:Person
    {
        private Specialization _specializationCode;

        public Specialization SpecializationCode { get => _specializationCode; set => _specializationCode = value; }
    }
}
