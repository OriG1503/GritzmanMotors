using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Role Controller
        [HttpGet]
        [ActionName("SelectAllRoles")]
        public async Task<RoleList> SelectAllRoles()
        {
            RoleDB dB = new RoleDB();
            RoleList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertRole")]
        public async Task<int> InsertRole([FromBody] Role role)
        {
            RoleDB dB = new RoleDB();
            dB.Insert(role);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateRole")]
        public async Task<int> UpdateRole([FromBody] Role role)
        {
            RoleDB db = new RoleDB();
            db.Update(role);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteRole")]
        public async Task<int> DeleteRole(int id)
        {
            Role role = await RoleDB.SelectById(id);
            RoleDB db = new RoleDB();
            db.Delete(role);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion
    }
}
