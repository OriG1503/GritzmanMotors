using System.Data;
using System.Data.OleDb;
using Model;
namespace ViewModel
{
    public abstract class BaseDB
    {
        #region OleDb Types - Class Types 
        //כתובת התחברות למסד נתונים
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location+ "/../../../../../../../GritzmanMotorsVS/ViewModel/GritzmanMotorsDB.accdb");

        //משתנה המייצג חיבור למסד הנתונים. זהו החיבור שישמש לבצע פעולות שונות במסד הנתונים
        protected static OleDbConnection connection;

        //משתנה המייצג פקודת אס-קיו-אל שתשלח למסד הנתונים, היא יכולה להיות פקודת הכנסה, עדכון, מחיקה או שאילתת בחירה
        protected OleDbCommand command;

        //משתנה המייצג את תוצאות השאילתא שנשלחה
        protected OleDbDataReader reader;
        #endregion

        #region Constructor

        //אנו מאתחלים את החיבור למסד הנתונים והפקודה שמשמשים לתקשורת עם המסד
        //זאת על מנת להבטיח קיום חיבור פתוח ומוכן לשימוש בכל פעם שנרצה לבצע פעולות במסד הנתונים
        public BaseDB()
        {
            connection ??= new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }
        #endregion

        #region Inserted Updated Deleted
        //אנו משתמשים ברשימות סטטיות כדי לעקוב אחר הרשומות שהוספו, שעודכנו או שנמחקו ממסד הנתונים
        //הרשימות האלו הן רשימות של כל הפעולות שאמורות להתבצע בכל אחת מהפעולות
        public static List<ChangeEntity> inserted = new List<ChangeEntity>();
        public static List<ChangeEntity> updated = new List<ChangeEntity>();
        public static List<ChangeEntity> deleted = new List<ChangeEntity>();
        #endregion

        #region Create [Insert/Update/Delete] SQL
        // פעולות אבסטרקטיות שמייצגות יצירת שאילתות הוספה, עדכון ומחיקה של רשומות במסד הנתונים
        protected abstract void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd);
        #endregion

        #region New Entity
        //פעולה אבסטרקטית שמטרתה ליצור ישות חדשה מסוג BaseEntity
        protected abstract BaseEntity NewEntity();
        #endregion

        #region Create Model
        // פעולה זו מקבלת ישות מסוימת ומעדכנת את המזהה שלה בהתאם לערך
        // שנמצא בקריאה האחרונה מהמסד נתונים, ולבסוף מחזירה את הישות עם המזהה המעודכן
        protected virtual async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }
        #endregion

        #region Insert Update Delete - Functions
        //כל פונקציה בודקת אם הישות המועברת תואמת את סוג הישות המצויין עבור המחלקה המתאימה
        //אם כן, הישות מתווספת לרשימה המתאימה להוספה, עדכון או מחיקה.
        public virtual void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity)); ;
            }
        }

        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion

        #region Select - Function
        //זוהי פעולה המבצעת פעולת בחירה ממסד הנתונים. היא מחזירה רשימה של ישויות מסוג
        //BaseEntity
        //מתוך התוצאות שנשלפו מהמסד
        protected async Task<List<BaseEntity>> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                if(connection.State != ConnectionState.Open)
                    connection.Open();

                this.reader = (OleDbDataReader)await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(await CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return list;
        }
        #endregion

        #region Save Changes - Function
        //פונקציה זו שומרת את השינויים במסד הנתונים על פי הרשימות של הישויות
        // שהוכנסו, עודכנו או נמחקו. היא מחזירה את מספר הרשומות ששונו בהצלחה
        public async Task<int> SaveChanges()
        {
            int records_affected = 0;
            try
            {
                if(connection.State != ConnectionState.Open )
                    connection.Open();

                foreach (var item in inserted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += await command.ExecuteNonQueryAsync();

                    command.CommandText = "Select @@Identity";
                    var y =await command.ExecuteScalarAsync();
                    int x = (int)y;
                    item.Entity.Id = x;
                }

                foreach (var item in updated)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += await command.ExecuteNonQueryAsync();
                }

                foreach (var item in deleted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                inserted.Clear();
                updated.Clear();
                deleted.Clear();

                if(reader != null)
                    reader.Close();
            }

            return records_affected;
        }
        #endregion
    }
}