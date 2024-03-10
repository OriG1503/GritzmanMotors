using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
    
        #region Employee Controller
        [HttpGet]
        [ActionName("SelectAllEmployees")]
        public async Task<EmployeeList> SelectAllEmployees()
        {
            EmployeeDB dB = new EmployeeDB();
            EmployeeList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertEmployee")]
        public async Task<int> InsertEmployee([FromBody] Employee employee)
        {
            EmployeeDB dB = new EmployeeDB();
            dB.Insert(employee);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateEmployee")]
        public async Task<int> UpdateEmployee([FromBody] Employee employee)
        {
            EmployeeDB db = new EmployeeDB();
            db.Update(employee);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteEmployee")]
        public async Task<int> DeleteEmployee(int id)
        {
            Employee employee = await EmployeeDB.SelectById(id);
            EmployeeDB db = new EmployeeDB();
            db.Delete(employee);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion

    }
}
