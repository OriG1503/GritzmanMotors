using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class PricingDB : BaseDB
    {
        #region Pricing List
        private static PricingList list = new PricingList();
        #endregion

        #region Constructor
        public PricingDB() : base() { }
        #endregion

        #region New Entity
        //הפעולה מחזירה איבר חדש מסוג
        //Pricing
        //בתור ישות מסוג
        //BaseEntity
        protected override BaseEntity NewEntity()
        {
            return new Pricing() as BaseEntity;
        }
        #endregion

        #region Create Model
        //הפעולה יוצרת עצם מסוג
        //Pricing
        //מתוך תוצאת שאילתת השליפה/בחירה ממסד הנתונים
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Pricing pricing = entity as Pricing;
            await base.CreateModel(entity);

            int idCarModel = (int)reader["modelCode"];
            pricing.ModelCode = await CarModelDB.SelectById(idCarModel);

            pricing.Price = double.Parse(reader["price"].ToString());
            return pricing;
        }
        #endregion

        #region Select All
        //הפעולה שולפת את כל הרשומות מהטבלה
        //שנמצאת במסד הנתונים PricingTBL
        public async Task<PricingList> SelectAll()
        {
            command.CommandText = "SELECT PricingTBL.id, PricingTBL.modelCode, PricingTBL.price, " +
                "CarModelTBL.companyCode FROM CarModelTBL INNER JOIN PricingTBL ON CarModelTBL.id = PricingTBL.modelCode;";
            list = new PricingList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        //הפעולה מבצעת שאילתת שליפה לרשומה מסוימת מהטבלה לפי ה
        //id
        public async static Task<Pricing> SelectById(int id)
        {
            if (list.Count == 0)
            {
                PricingDB dB = new PricingDB();
                list = await dB.SelectAll();
            }

            Pricing pricing = list.Find(item => item.Id == id);
            return pricing;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        //שלושת הפעולות יוצרות את הפקודות המתאימות להוספה, עדכון ומחיקה של רשומות מהטבלה במסד הנתונים

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Pricing pricing = entity as Pricing;

            if (pricing != null)
            {
                cmd.CommandText = "INSERT INTO PricingTBL (modelCode, price) VALUES (@ModelCode, @Price)";
                cmd.Parameters.Add(new OleDbParameter("@ModelCode", pricing.ModelCode.Id));
                cmd.Parameters.Add(new OleDbParameter("@Price", pricing.Price));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Pricing pricing = entity as Pricing;
            cmd.CommandText = $"UPDATE PricingTBL SET modelCode=@ModelCode, price=@Price WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@ModelCode", pricing.ModelCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@Price", pricing.Price));
            cmd.Parameters.Add(new OleDbParameter("@Id", pricing.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Pricing pricing = entity as Pricing;
            cmd.CommandText = $"DELETE FROM PricingTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", pricing.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        //שלושת הפעולות מוסיפות, מעדכנות ומוחקות רשומות מהטבלה במסד הנתונים
        public override void Insert(BaseEntity entity)
        {
            Pricing reqEntity = entity as Pricing;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Pricing pricing = entity as Pricing;
            if (pricing != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Pricing pricing = entity as Pricing;
            if (pricing != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
