using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Customer Controller
        [HttpGet]
        [ActionName("SelectAllCustomers")]
        public async Task<CustomerList> SelectAllCustomers()
        {
            CustomerDB dB = new CustomerDB();
            CustomerList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertCustomer")]
        public async Task<int> InsertCustomer([FromBody] Customer customer)
        {
            CustomerDB dB = new CustomerDB();
            dB.Insert(customer);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateCustomer")]
        public async Task<int> UpdateCustomer([FromBody] Customer customer)
        {
            CustomerDB db = new CustomerDB();
            db.Update(customer);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteCustomer")]
        public async Task<int> DeleteCustomer(int id)
        {
            Customer customer = await CustomerDB.SelectById(id);
            CustomerDB db = new CustomerDB();
            db.Delete(customer);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion

    }
}
