using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace ViewModel
{
    public class ManagerDB : PersonDB
    {
        #region Manager List
        private static ManagerList list = new ManagerList();
        #endregion

        #region Constructor
        public ManagerDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new Manager() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Manager manager = entity as Manager;
            await base.CreateModel(entity);

            int idRoleCode = (int)reader["roleCode"];
            manager.RoleCode = await RoleDB.SelectById(idRoleCode);
            return manager;
        }
        #endregion

        #region Select All
        public async Task<ManagerList> SelectAll()
        {
            command.CommandText = "SELECT PersonTBL.id, PersonTBL.firstName, PersonTBL.lastName, PersonTBL.dateOfBirth, ManagerTBL.roleCode FROM PersonTBL INNER JOIN ManagerTBL ON PersonTBL.id = ManagerTBL.id;";
            list = new ManagerList(await base.Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<Manager> SelectById(int id)
        {
            if (list.Count == 0)
            {
                ManagerDB dB = new ManagerDB();
                list = await dB.SelectAll();
            }

            Manager manager = list.Find(item => item.Id == id);
            return manager;
        }
        #endregion

        //אני חושב שיש טעויות בריג'יון שלמטה
        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager manager = entity as Manager;

            if (manager != null)
            {
                cmd.CommandText = "INSERT INTO ManagerTBL (id, roleCode) VALUES (@Id, @RoleCode)";
                cmd.Parameters.Add(new OleDbParameter("@Id", manager.Id));
                cmd.Parameters.Add(new OleDbParameter("@RoleCode", manager.RoleCode.Id));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager manager = entity as Manager;

            cmd.CommandText = $"UPDATE ManagerTBL SET roleCode=@RoleCode WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@RoleCode", manager.RoleCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@Id", manager.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager manager = entity as Manager;
            cmd.CommandText = $"DELETE FROM ManagerTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", manager.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            Manager reqEntity = entity as Manager;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Manager customer = entity as Manager;
            if (customer != null)
            {
                updated.Add(new ChangeEntity(base.CreateUpdateSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Manager customer = entity as Manager;
            if (customer != null)
            {
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
