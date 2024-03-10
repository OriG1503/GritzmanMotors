﻿using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SpecializationDB : BaseDB
    {
        #region Specialization List
        private static SpecializationList list = new SpecializationList();
        #endregion

        #region Constructor
        public SpecializationDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new Specialization() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Specialization specialization = entity as Specialization;
            await base.CreateModel(entity);
            specialization.SpecializationName = reader["specializationName"].ToString();
            return specialization;
        }
        #endregion

        #region Select All
        public async Task<SpecializationList> SelectAll()
        {
            command.CommandText = "SELECT * FROM SpecializationTBL";
            list = new SpecializationList(await Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<Specialization> SelectById(int id)
        {
            if (list.Count == 0)
            {
                SpecializationDB dB = new SpecializationDB();
                list = await dB.SelectAll();
            }

            Specialization specialization = list.Find(item => item.Id == id);
            return specialization;
        }
        #endregion

        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Specialization specialization = entity as Specialization;

            if (specialization != null)
            {
                cmd.CommandText = "INSERT INTO SpecializationTBL (specializationName) VALUES (@SpecializationName)";
                cmd.Parameters.Add(new OleDbParameter("@SpecializationName", specialization.SpecializationName));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Specialization specialization = entity as Specialization;

            cmd.CommandText = $"UPDATE SpecializationTBL SET specializationName=@SpecializationName WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@SpecializationName", specialization.SpecializationName));
            cmd.Parameters.Add(new OleDbParameter("@Id", specialization.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Specialization specialization = entity as Specialization;

            cmd.CommandText = $"DELETE FROM SpecializationTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", specialization.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            Specialization specialization = entity as Specialization;
            if (specialization != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Specialization specialization = entity as Specialization;
            if (specialization != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Specialization specialization = entity as Specialization;
            if (specialization != null)
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
