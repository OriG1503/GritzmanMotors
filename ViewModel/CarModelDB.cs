using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CarModelDB : BaseDB
    {
        #region Car Model List
        private static CarModelList list = new CarModelList();
        #endregion

        #region Constructor
        public CarModelDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new CarModel() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            CarModel carModel = entity as CarModel;
            await base.CreateModel(entity);

            int idCarCompany = (int)reader["companyCode"];
            carModel.CompanyCode = await CarCompanyDB.SelectById(idCarCompany);

            carModel.CarModelName = reader["carModelName"].ToString();
            return carModel;
        }
        #endregion

        #region Select All
        public async Task<CarModelList> SelectAll()
        {
            command.CommandText = "SELECT * FROM CarModelTBL";
            list = new CarModelList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<CarModel> SelectById(int id)
        {
            if (list.Count == 0)
            {
                CarModelDB dB = new CarModelDB();
                list = await dB.SelectAll();
            }

            CarModel carModel = list.Find(item => item.Id == id);
            return carModel;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarModel carModel = entity as CarModel;

            if (carModel != null)
            {
                cmd.CommandText = "INSERT INTO CarModelTBL (companyCode, carModelName) VALUES (@CompanyCode, @CarModelName)";
                cmd.Parameters.Add(new OleDbParameter("@CompanyCode", carModel.CompanyCode.Id));
                cmd.Parameters.Add(new OleDbParameter("@CarModelName", carModel.CarModelName));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarModel carModel = entity as CarModel;
            cmd.CommandText = $"UPDATE CarModelTBL SET companyCode=@CompanyCode, carModelName=@CarModelName WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@CompanyCode", carModel.CompanyCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@CarModelName", carModel.CarModelName));
            cmd.Parameters.Add(new OleDbParameter("@Id", carModel.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            CarModel carModel = entity as CarModel;
            cmd.CommandText = $"DELETE FROM CarModelTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", carModel.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            CarModel reqEntity = entity as CarModel;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            CarModel carModel = entity as CarModel;
            if (carModel != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            CarModel carModel = entity as CarModel;
            if (carModel != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
