﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class CustomerDB : PersonDB
    {
        #region Customer List
        private static CustomerList list = new CustomerList();
        #endregion

        #region Constructor
        public CustomerDB() : base() { }
        #endregion

        #region New Entity
        protected override BaseEntity NewEntity()
        {
            return new Customer() as BaseEntity;
        }
        #endregion

        #region Create Model
        protected override async Task<BaseEntity> CreateModel(BaseEntity entity)
        {
            Customer customer = entity as Customer;
            await base.CreateModel(entity);

            customer.PhoneNumber = reader["phoneNumber"].ToString();
            return customer;
        }
        #endregion

        #region Select All
        public async Task<CustomerList> SelectAll()
        {
            command.CommandText = "SELECT CustomerTBL.id, CustomerTBL.phoneNumber, PersonTBL.firstName, PersonTBL.lastName, PersonTBL.dateOfBirth FROM PersonTBL INNER JOIN CustomerTBL ON PersonTBL.id = CustomerTBL.id;";
            list = new CustomerList(await base.Select());
            return list;
        }
        #endregion

        #region Select By Id
        public async static Task<Customer> SelectById(int id)
        {
            if (list.Count == 0)
            {
                CustomerDB dB = new CustomerDB();
                list = await dB.SelectAll();
            }

            Customer customer = list.Find(item => item.Id == id);
            return customer;
        }
        #endregion

        //אני חושב שיש טעויות בריג'יון שלמטה
        #region Create [Insert/Update/Delete] SQL
        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Customer customer = entity as Customer;

            if (customer != null)
            {
                cmd.CommandText = "INSERT INTO CustomerTBL (id, phoneNumber) VALUES (@Id, @PhoneNumber)";
                cmd.Parameters.Add(new OleDbParameter("@Id", customer.Id));
                cmd.Parameters.Add(new OleDbParameter("@PhoneNumber", customer.PhoneNumber));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Customer customer = entity as Customer;

            cmd.CommandText = $"UPDATE CustomerTBL SET phoneNumber=@PhoneNumber WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@PhoneNumber", customer.PhoneNumber));
            cmd.Parameters.Add(new OleDbParameter("@Id", customer.Id));
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Customer customer = entity as Customer;
            cmd.CommandText = $"DELETE FROM CustomerTBL WHERE id = @Id";
            cmd.Parameters.Add(new OleDbParameter("@Id", customer.Id));
        }
        #endregion

        #region Insert Update Delete - Functions
        public override void Insert(BaseEntity entity)
        {
            Customer reqEntity = entity as Customer;
            if (reqEntity != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }

        public override void Update(BaseEntity entity)
        {
            Customer customer = entity as Customer;
            if (customer != null)
            {
                updated.Add(new ChangeEntity(base.CreateUpdateSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            Customer customer = entity as Customer;
            if (customer != null)
            {
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        #endregion
    }
}
