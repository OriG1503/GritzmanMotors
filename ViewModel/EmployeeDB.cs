using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class EmployeeDB : PersonDB
    {
        #region Employee List
        private static EmployeeList list = new EmployeeList();
        #endregion

        #region Constructor
        public EmployeeDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new Employee() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Employee employee = entity as Employee;
            await base.CreateModel(entity);

            int idSpecialization = (int)reader["specializationCode"];
            employee.SpecializationCode = await SpecializationDB.SelectById(idSpecialization);
            return employee;
        }
        #endregion

        #region Select All
        public async Task<EmployeeList> SelectAll()
        {
            command.CommandText = "SELECT PersonTBL.id, PersonTBL.firstName, PersonTBL.lastName, PersonTBL.dateOfBirth, EmployeeTBL.specializationCode FROM PersonTBL INNER JOIN EmployeeTBL ON PersonTBL.id = EmployeeTBL.id;";
            list = new EmployeeList(await base.Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<Employee> SelectById(int id)
        {
            if (list.Count == 0)
            {
                EmployeeDB dB = new EmployeeDB();
                list = await dB.SelectAll();
            }

            Employee employee = list.Find(item => item.Id == id);
            return employee;
        }
        #endregion

        //אני חושב שיש טעויות בריג'יון שלמטה
        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Employee employee = entity as Employee;

            if (employee != null)
            {
                cmd.CommandText = "INSERT INTO EmployeeTBL (id, specializationCode) VALUES (@Id, @SpecializationCode)";
                cmd.Parameters.Add(new OleDbParameter("@Id", employee.Id));
                cmd.Parameters.Add(new OleDbParameter("@SpecializationCode", employee.SpecializationCode.Id));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Employee employee = entity as Employee;

            cmd.CommandText = $"UPDATE EmployeeTBL SET specializationCode=@SpecializationCode WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@SpecializationCode", employee.SpecializationCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@Id", employee.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Employee employee = entity as Employee;
            cmd.CommandText = $"DELETE FROM EmployeeTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", employee.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            Employee reqEntity = entity as Employee;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Employee customer = entity as Employee;
            if (customer != null)
            {
                updated.Add(new ChangeEntity(base.CreateUpdateSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Employee customer = entity as Employee;
            if (customer != null)
            {
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
