using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Model;
using System.Security.Claims;

namespace ViewModel
{
    public class CarCompanyDB : BaseDB
    {
        #region Car Company List
        private static CarCompanyList list = new CarCompanyList();
        #endregion

        #region Constructor
        public CarCompanyDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new CarCompany() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            CarCompany carCompany = entity as CarCompany;
            await base.CreateModel(entity);
            carCompany.CarCompanyName = reader["carCompanyName"].ToString();
            return carCompany;
        }
        #endregion

        #region Select All
        public async Task<CarCompanyList> SelectAll()
        {
            command.CommandText = "SELECT * FROM CarCompanyTBL";
            list = new CarCompanyList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<CarCompany> SelectById(int id)
        {
            if (list.Count == 0)
            {
                CarCompanyDB dB = new CarCompanyDB();
                list = await dB.SelectAll();
            }

            CarCompany carCompany = list.Find(item => item.Id == id);
            return carCompany;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarCompany carCompany = entity as CarCompany;

            if (carCompany != null) 
            {
                cmd.CommandText = "INSERT INTO CarCompanyTBL (carCompanyName) VALUES (@CarCompanyName)";
                cmd.Parameters.Add(new OleDbParameter("@CarCompanyName", carCompany.CarCompanyName));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarCompany carCompany = entity as CarCompany;

            cmd.CommandText = $"UPDATE CarCompanyTBL SET carCompanyName=@CarCompanyName WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@CarCompanyName", carCompany.CarCompanyName));
            cmd.Parameters.Add(new OleDbParameter("@Id", carCompany.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarCompany carCompany = entity as CarCompany;
            cmd.CommandText = $"DELETE FROM CarCompanyTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", carCompany.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            CarCompany reqEntity = entity as CarCompany;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            CarCompany carCompany = entity as CarCompany;
            if (carCompany != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            CarCompany carCompany = entity as CarCompany;
            if (carCompany != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
