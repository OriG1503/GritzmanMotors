using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Customer:Person
    {
        private string _phoneNumber;

        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
    }
}
