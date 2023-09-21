using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order:BaseEntity
    {
        private Pricing _priceCode;
        private Customer _customerCode;
        private Employee _employeeCode;
        private DateOnly _dateOfTreatment;
        private bool _carReady;
        private DateTime _dateOfOrder;

        public Employee EmployeeCode { get => _employeeCode; set => _employeeCode = value; }
        public DateOnly DateOfTreatment { get => _dateOfTreatment; set => _dateOfTreatment = value; }
        public bool CarReady { get => _carReady; set => _carReady = value; }
        public DateTime DateOfOrder { get => _dateOfOrder; set => _dateOfOrder = value; }
        public Pricing PriceCode { get => _priceCode; set => _priceCode = value; }
        public Customer CustomerCode { get => _customerCode; set => _customerCode = value; }
    }
}
