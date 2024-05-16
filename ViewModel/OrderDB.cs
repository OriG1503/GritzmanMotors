using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class OrderDB : BaseDB
    {
        #region Order List
        private static OrderList list = new OrderList();
        #endregion

        #region Constructor
        public OrderDB() : base() { }
        #endregion

        #region New Entity
        //הפעולה מחזירה איבר חדש מסוג
        //Order
        //בתור ישות מסוג
        //BaseEntity
        protected override BaseEntity NewEntity()
        {
            return new Order() as BaseEntity;
        }
        #endregion

        #region Create Model
        //הפעולה יוצרת עצם מסוג
        //Order
        //מתוך תוצאת שאילתת השליפה/בחירה ממסד הנתונים
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Order order = entity as Order;
            await base.CreateModel(entity);

            int idPrice = (int)reader["priceCode"];
            order.PriceCode = await PricingDB.SelectById(idPrice);

            int idEmployee = (int)reader["employeeCode"];
            order.EmployeeCode = await EmployeeDB.SelectById(idEmployee);

            int idCustomer = (int)reader["CustomerCode"];
            order.CustomerCode = await CustomerDB.SelectById(idCustomer);

            order.CarReady = bool.Parse(reader["carReady"].ToString());
            order.DateOfTreatment = DateOnly.FromDateTime(DateTime.Parse(reader["dateOfTreatment"].ToString()));
            order.DateOfOrder = DateTime.Parse(reader["dateOfOrder"].ToString());

            return order;
        }
        #endregion

        #region Select All
        //הפעולה שולפת את כל הרשומות מהטבלה
        //שנמצאת במסד הנתונים OrderTBL 
        public async Task<OrderList> SelectAll()
        {
            command.CommandText = "SELECT OrderTBL.id, OrderTBL.priceCode, OrderTBL.customerCode, " +
                "OrderTBL.employeeCode, OrderTBL.dateOfTreatment, OrderTBL.carReady, OrderTBL.dateOfOrder FROM OrderTBL;";
            list = new OrderList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        //הפעולה מבצעת שאילתת שליפה לרשומה מסוימת מהטבלה לפי ה
        //id
        public async static Task<Order> SelectById(int id)
        {
            if (list.Count == 0)
            {
                OrderDB dB = new OrderDB();
                list = await dB.SelectAll();
            }

            Order order = list.Find(item => item.Id == id);
            return order;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        //שלושת הפעולות יוצרות את הפקודות המתאימות להוספה, עדכון ומחיקה של רשומות מהטבלה במסד הנתונים
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Order order = entity as Order;

            if (order != null)
            {
                if (order.EmployeeCode?.Id != null)
                {
                    cmd.CommandText = "INSERT INTO OrderTBL (priceCode, customerCode, employeeCode, dateOfTreatment, carReady, dateOfOrder)" +
                        " VALUES (@PriceCode, @CustomerCode, @EmployeeCode, @DateOfTreatment, @CarReady, @DateOfOrder)";
                    cmd.Parameters.Add(new OleDbParameter("@PriceCode", order.PriceCode.Id));
                    cmd.Parameters.Add(new OleDbParameter("@CustomerCode", order.CustomerCode.Id));
                    cmd.Parameters.Add(new OleDbParameter("@EmployeeCode", order.EmployeeCode?.Id));
                    cmd.Parameters.Add(new OleDbParameter("@DateOfTreatment", DateTime.Parse(order.DateOfTreatment.ToString()).Date));
                    cmd.Parameters.Add(new OleDbParameter("@CarReady", order.CarReady));
                    cmd.Parameters.Add(new OleDbParameter("@DateOfOrder", DateTime.Parse(order.DateOfOrder.ToString())));
                }
                else
                {
                    cmd.CommandText = "INSERT INTO OrderTBL (priceCode, customerCode, dateOfTreatment, carReady, dateOfOrder) " +
                        "VALUES (@PriceCode, @CustomerCode, @DateOfTreatment, @CarReady, @DateOfOrder)";
                    cmd.Parameters.Add(new OleDbParameter("@PriceCode", order.PriceCode.Id));
                    cmd.Parameters.Add(new OleDbParameter("@CustomerCode", order.CustomerCode.Id));
                    cmd.Parameters.Add(new OleDbParameter("@DateOfTreatment", DateTime.Parse(order.DateOfTreatment.ToString()).Date));
                    cmd.Parameters.Add(new OleDbParameter("@CarReady", order.CarReady));
                    cmd.Parameters.Add(new OleDbParameter("@DateOfOrder", DateTime.Parse(order.DateOfOrder.ToString())));
                }
                
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Order order = entity as Order;
            cmd.CommandText = $"UPDATE OrderTBL SET priceCode=@PriceCode, customerCode=@CustomerCode, employeeCode=@EmployeeCode," +
                $" dateOfTreatment=@DateOfTreatment, carReady=@CarReady, dateOfOrder=@DateOfOrder WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@PriceCode", order.PriceCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@CustomerCode", order.CustomerCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@EmployeeCode", order.EmployeeCode.Id));
            cmd.Parameters.Add(new OleDbParameter("@DateOfTreatment", DateTime.Parse(order.DateOfTreatment.ToString()).Date));
            cmd.Parameters.Add(new OleDbParameter("@CarReady", order.CarReady));
            cmd.Parameters.Add(new OleDbParameter("@DateOfOrder", DateTime.Parse(order.DateOfOrder.ToString())));
            cmd.Parameters.Add(new OleDbParameter("@Id", order.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Order order = entity as Order;
            cmd.CommandText = $"DELETE FROM OrderTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", order.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        //שלושת הפעולות מוסיפות, מעדכנות ומוחקות רשומות מהטבלה במסד הנתונים
        public override void Insert(BaseEntity entity)
        {
            Order reqEntity = entity as Order;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Order order = entity as Order;
            if (order != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Order order = entity as Order;
            if (order != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
