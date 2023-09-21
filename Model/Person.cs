using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person:BaseEntity
    {
        private string _firstName;
        private string _lastName;
        private DateOnly _dateOfBirth;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateOnly DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
    }
}
