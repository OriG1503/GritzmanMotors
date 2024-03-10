using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        #region CarModel Controller
        [HttpGet]
        [ActionName("SelectAllCarModels")]
        public async Task<CarModelList> SelectAllCarModels()
        {
            CarModelDB dB = new CarModelDB();
            CarModelList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertCarModel")]
        public async Task<int> InsertCarModel([FromBody] CarModel carModel)
        {
            CarModelDB dB = new CarModelDB();
            dB.Insert(carModel);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateCarModel")]
        public async Task<int> UpdateCarModel([FromBody] CarModel carModel)
        {
            CarModelDB db = new CarModelDB();
            db.Update(carModel);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteCarModel")]
        public async Task<int> DeleteCarModel(int id)
        {
            CarModel carModel = await CarModelDB.SelectById(id);
            CarModelDB db = new CarModelDB();
            db.Delete(carModel);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion
    }
}
