using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        #region Pricing Controller
        [HttpGet]
        [ActionName("SelectAllPricings")]
        public async Task<PricingList> SelectAllPricings()
        {
            PricingDB dB = new PricingDB();
            PricingList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertPricing")]
        public async Task<int> InsertPricing([FromBody] Pricing price)
        {
            PricingDB dB = new PricingDB();
            dB.Insert(price);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdatePricing")]
        public async Task<int> UpdatePricing([FromBody] Pricing price)
        {
            PricingDB db = new PricingDB();
            db.Update(price);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeletePricing")]
        public async Task<int> DeletePricing(int id)
        {
            Pricing price = await PricingDB.SelectById(id);
            PricingDB db = new PricingDB();
            db.Delete(price);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion
    }
}
