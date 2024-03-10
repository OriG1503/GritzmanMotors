using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarCompanyController : ControllerBase
    {
        #region CarCompany Controller
        [HttpGet]
        [ActionName("SelectAllCarCompanies")]
        public async Task<CarCompanyList> SelectAllCarCompanies()
        {
            CarCompanyDB dB = new CarCompanyDB();
            CarCompanyList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertCarCompany")]
        public async Task<int> InsertCarCompany([FromBody] CarCompany carCompany)
        {
            CarCompanyDB dB = new CarCompanyDB();
            dB.Insert(carCompany);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateCarCompany")]
        public async Task<int> UpdateCarCompany([FromBody] CarCompany carCompany)
        {
            CarCompanyDB db = new CarCompanyDB();
            db.Update(carCompany);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteCarCompany")]
        public async Task<int> DeleteCarCompany(int id)
        {
            CarCompany carCompany = await CarCompanyDB.SelectById(id);
            CarCompanyDB db = new CarCompanyDB();
            db.Delete(carCompany);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion
    }
}
