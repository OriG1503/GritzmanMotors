using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CarModelList:List<CarModel>
    {
        public CarModelList() { }
    
        public CarModelList(IEnumerable<CarModel> list) : base(list) { }

        public CarModelList(IEnumerable<BaseEntity> list) : base(list.Cast<CarModel>().ToList()) { }
    }
}
