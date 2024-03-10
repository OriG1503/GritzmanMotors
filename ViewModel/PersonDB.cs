using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PersonDB : BaseDB
    {
        #region Person List
        private static PersonList list = new PersonList();
        #endregion

        #region Constructor
        public PersonDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new Person() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Person person = entity as Person;
            await base.CreateModel(entity);

            person.FirstName = reader["firstName"].ToString();
            person.LastName = reader["lastName"].ToString();
            person.DateOfBirth =DateOnly.FromDateTime(DateTime.Parse( reader["dateOfBirth"].ToString()));
            return person;
        }
        #endregion

        #region Select All
        public async Task<PersonList> SelectAll()
        {
            command.CommandText = "SELECT * FROM PersonTBL";
            list = new PersonList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<Person> SelectById(int id)
        {
            if (list.Count == 0)
            {
                PersonDB dB = new PersonDB();
                list = await dB.SelectAll();
            }

            Person person = list.Find(item => item.Id == id);
            return person;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person person = entity as Person;

            if (person != null)
            {
                cmd.CommandText = "INSERT INTO PersonTBL (firstName, lastName, dateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth)";
                cmd.Parameters.Add(new OleDbParameter("@FirstName", person.FirstName));
                cmd.Parameters.Add(new OleDbParameter("@LastName", person.LastName));
                cmd.Parameters.Add(new OleDbParameter("@DateOfBirth", DateTime.Parse(person.DateOfBirth.ToString()).Date));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person person = entity as Person;

            cmd.CommandText = $"UPDATE PersonTBL SET firstName=@FirstName, lastName=@LastName, dateOfBirth=@DateOfBirth WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@FirstName", person.FirstName));
            cmd.Parameters.Add(new OleDbParameter("@LastName", person.LastName));
            cmd.Parameters.Add(new OleDbParameter("@DateOfBirth", DateTime.Parse(person.DateOfBirth.ToString()).Date));
            cmd.Parameters.Add(new OleDbParameter("@Id", person.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Person person = entity as Person;
            cmd.CommandText = $"DELETE FROM PersonTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", person.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
