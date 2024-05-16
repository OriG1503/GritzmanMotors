using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class RoleDB : BaseDB
    {
        #region Role List
        private static RoleList list = new RoleList();
        #endregion

        #region Constructor
        public RoleDB() : base() { }
        #endregion

        #region New Entity
        //הפעולה מחזירה איבר חדש מסוג
        //Role
        //בתור ישות מסוג
        //BaseEntity
        protected override BaseEntity NewEntity()
        {
            return new Role() as BaseEntity;
        }
        #endregion

        #region Create Model
        //הפעולה יוצרת עצם מסוג
        //Role
        //מתוך תוצאת שאילתת השליפה/בחירה ממסד הנתונים
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Role role = entity as Role;
            await base.CreateModel(entity);
            role.RoleName = reader["roleName"].ToString();
            return role;
        }
        #endregion

        #region Select All
        //הפעולה שולפת את כל הרשומות מהטבלה
        //שנמצאת במסד הנתונים RoleTBL 
        public async Task<RoleList> SelectAll()
        {
            command.CommandText = "SELECT * FROM RoleTBL";
            list = new RoleList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        //הפעולה מבצעת שאילתת שליפה לרשומה מסוימת מהטבלה לפי ה
        //id
        public async static Task<Role> SelectById(int id)
        {
            if (list.Count == 0)
            {
                RoleDB dB = new RoleDB();
                list = await dB.SelectAll();
            }

            Role role = list.Find(item => item.Id == id);
            return role;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        //שלושת הפעולות יוצרות את הפקודות המתאימות להוספה, עדכון ומחיקה של רשומות מהטבלה במסד הנתונים
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Role role = entity as Role;

            if (role != null)
            {
                cmd.CommandText = "INSERT INTO RoleTBL (roleName) VALUES (@RoleName)";
                cmd.Parameters.Add(new OleDbParameter("@RoleName", role.RoleName));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Role role = entity as Role;

            cmd.CommandText = $"UPDATE RoleTBL SET roleName=@RoleName WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@RoleName", role.RoleName));
            cmd.Parameters.Add(new OleDbParameter("@Id", role.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Role role = entity as Role;
            cmd.CommandText = $"DELETE FROM RoleTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", role.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        //שלושת הפעולות מוסיפות, מעדכנות ומוחקות רשומות מהטבלה במסד הנתונים
        public override void Insert(BaseEntity entity)
        {
            Role role = entity as Role;
            if (role != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Role role = entity as Role;
            if (role != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Role role = entity as Role;
            if (role != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
