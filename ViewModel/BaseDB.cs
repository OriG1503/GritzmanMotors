using System.Data;
using System.Data.OleDb;
using Model;
namespace ViewModel
{
    public abstract class BaseDB
    {
        #region OleDb Types - Class Types 
        //כתובת בבית
        //protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ori.G\Desktop\Ori's School\כיתה יב\הנדסת תוכנה\GritzmanMotorsVS\ViewModel\GritzmanMotorsDB.accdb";

        // כתובת בבית ספר
        //protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\אורי גריצמן\GritzmanMotorsVS\ViewModel\GritzmanMotorsDB.accdb";

        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location+ "/../../../../../../../GritzmanMotorsVS/ViewModel/GritzmanMotorsDB.accdb");
        protected static OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        #endregion

        #region Constructor
        public BaseDB()
        {
            connection ??= new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }
        #endregion

        #region Inserted Updated Deleted
        public static List<ChangeEntity> inserted = new List<ChangeEntity>();
        public static List<ChangeEntity> updated = new List<ChangeEntity>();
        public static List<ChangeEntity> deleted = new List<ChangeEntity>();
        #endregion

        #region Create [Insert/Update/Delete] SQL
        protected abstract void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd);
        #endregion

        #region New Entity
        protected abstract BaseEntity NewEntity();
        #endregion

        #region Create Model
        protected virtual async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }
        #endregion

        #region Insert Update Delete - Functions
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
        protected async Task<List<BaseEntity>> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                //command.Connection = connection; 
                //connection.Close();
                //connection.Open();

                if(connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

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

                //if (connection.State == System.Data.ConnectionState.Open)
                //    connection.Close();
            }

            return list;
        }
        #endregion

        #region Save Changes - Function
        public async Task<int> SaveChanges()
        {
            int records_affected = 0;
            try
            {
                //command.Connection = this.connection;
                //this.connection.Open();

                if(connection.State != ConnectionState.Open )
                {
                    connection.Open();
                }

                foreach (var item in inserted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += await command.ExecuteNonQueryAsync();

                    command.CommandText = "Select @@Identity";
                    var y =await command.ExecuteScalarAsync();
                    int x = (int)y;
                    //var x = (int)y.Result;

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
                {
                    reader.Close();
                }

                //if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            }

            return records_affected;
        }
        #endregion
    }
}