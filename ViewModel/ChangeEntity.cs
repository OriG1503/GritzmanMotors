using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class ChangeEntity
    {
        #region Class Types
        private CreateSql _createSql;
        private BaseEntity _entity;
        #endregion

        #region Consturctor
        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            this._createSql = createSql;
            this._entity = entity;
        }
        #endregion

        #region Getters && Setters
        public CreateSql CreateSql { get => _createSql; set => _createSql = value; }
        public BaseEntity Entity { get => _entity; set => _entity = value; }
        #endregion

    }
    public delegate void CreateSql(BaseEntity entity, OleDbCommand command);
}