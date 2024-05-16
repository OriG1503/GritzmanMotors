using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Order Controller
        [HttpGet]
        [ActionName("SelectAllOrders")]
        public async Task<OrderList> SelectAllOrders()
        {
            OrderDB dB = new OrderDB();
            OrderList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertOrder")]
        public async Task<int> InsertOrder([FromBody] Order order)
        {
            OrderDB dB = new OrderDB();
            dB.Insert(order);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateOrder")]
        public async Task<int> UpdateOrder([FromBody] Order order)
        {
            OrderDB db = new OrderDB();
            db.Update(order);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteOrder")]
        public async Task<int> DeleteOrder(int id)
        {
            Order order = await OrderDB.SelectById(id);
            OrderDB db = new OrderDB();
            db.Delete(order);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion
    }
}
