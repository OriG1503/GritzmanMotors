using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CarModel:BaseEntity
    {
        private CarCompany _companyCode;
        private string _carModelName;

        public CarCompany CompanyCode { get => _companyCode; set => _companyCode = value; }
        public string CarModelName { get => _carModelName; set => _carModelName = value; }
    }
}
