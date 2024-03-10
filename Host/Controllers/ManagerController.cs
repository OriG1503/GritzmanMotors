using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        #region Manager Controller
        [HttpGet]
        [ActionName("SelectAllManagers")]
        public async Task<ManagerList> SelectAllManagers()
        {
            ManagerDB dB = new ManagerDB();
            ManagerList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertManager")]
        public async Task<int> InsertManager([FromBody] Manager manager)
        {
            ManagerDB dB = new ManagerDB();
            dB.Insert(manager);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateManager")]
        public async Task<int> UpdateManager([FromBody] Manager manager)
        {
            ManagerDB db = new ManagerDB();
            db.Update(manager);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteManager")]
        public async Task<int> DeleteManager(int id)
        {
            Manager manager = await ManagerDB.SelectById(id);
            ManagerDB db = new ManagerDB();
            db.Delete(manager);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion

    }
}
