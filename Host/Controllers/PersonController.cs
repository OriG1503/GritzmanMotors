using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        #region Person Controller
        [HttpGet]
        [ActionName("SelectAllPersons")]
        public async Task<PersonList> SelectAllPersons()
        {
            PersonDB dB = new PersonDB();
            PersonList list = await dB.SelectAll();
            return list;
        }

        [HttpPost]
        [ActionName("InsertPerson")]
        public async Task<int> InsertPerson([FromBody] Person person)
        {
            PersonDB dB = new PersonDB();
            dB.Insert(person);
            int x = await dB.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdatePerson")]
        public async Task<int> UpdatePerson([FromBody] Person person)
        {
            PersonDB db = new PersonDB();
            db.Update(person);
            int x = await db.SaveChanges();
            return x;
        }

        [HttpDelete("{id}")]
        [ActionName("DeletePerson")]
        public async Task<int> DeletePerson(int id)
        {
            Person person = await PersonDB.SelectById(id);
            PersonDB db = new PersonDB();
            db.Delete(person);
            int x = await db.SaveChanges();
            return x;
        }
        #endregion

    }
}
