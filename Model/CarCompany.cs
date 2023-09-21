using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CarCompany:BaseEntity
    {
        private string _carCompanyName;

        public string CarCompanyName { get => _carCompanyName; set => _carCompanyName = value; }
    }
}
