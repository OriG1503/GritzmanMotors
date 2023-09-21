using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pricing:BaseEntity
    {
        private CarModel _modelCode;
        private double _price;

        public CarModel ModelCode { get => _modelCode; set => _modelCode = value; }
        public double Price { get => _price; set => _price = value; }
    }
}
