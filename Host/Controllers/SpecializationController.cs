using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        #region Specialization Controller
        [HttpGet]
        [ActionName("SelectAllSpecializations")]
        public async Task<SpecializationList> SelectAllSpecializations()
        {
            SpecializationDB dB = new SpecializationDB();
            SpecializationList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertSpecialization")]
        public async Task<int> InsertSpecialization([FromBody] Specialization specialization)
        {
            SpecializationDB dB = new SpecializationDB();
            dB.Insert(specialization);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateSpecialization")]
        public async Task<int> UpdateSpecialization([FromBody] Specialization specialization)
        {
            SpecializationDB db = new SpecializationDB();
            db.Update(specialization);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteSpecialization")]
        public async Task<int> DeleteSpecialization(int id)
        {
            Specialization specialization = await SpecializationDB.SelectById(id);
            SpecializationDB db = new SpecializationDB();
            db.Delete(specialization);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion

    }
}
