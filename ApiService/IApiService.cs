using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using Model;
namespace ApiServiceNM
{
    public interface IApiService
    {

        #region Person
        Task<PersonList> GetPersonList();
        Task<int> InsertPerson(Person person);
        Task<int> UpdatePerson(Person person);
        Task<int> DeletePerson(Person person);
        #endregion

        #region Employee
        Task<EmployeeList> GetEmployeeList();
        Task<int> InsertEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee employee);
        Task<int> DeleteEmployee(Employee employee);
        #endregion

        #region Manager
        Task<ManagerList> GetManagerList();
        Task<int> InsertManager(Manager manager);
        Task<int> UpdateManager(Manager manager);
        Task<int> DeleteManager(Manager manager);
        #endregion

        #region Customer
        Task<CustomerList> GetCustomerList();
        Task<int> InsertCustomer(Customer customer);
        Task<int> UpdateCustomer(Customer customer);
        Task<int> DeleteCustomer(Customer customer);
        #endregion

        #region Specialization
        Task<SpecializationList> GetSpecializationList();
        Task<int> InsertSpecialization(Specialization specialization);
        Task<int> UpdateSpecialization(Specialization specialization);
        Task<int> DeleteSpecialization(Specialization specialization);
        #endregion

        #region Role
        Task<RoleList> GetRoleList();
        Task<int> InsertRole(Role role);
        Task<int> UpdateRole(Role role);
        Task<int> DeleteRole(Role role);
        #endregion

        #region Car Company
        Task<CarCompanyList> GetCarCompanyList();
        Task<int> InsertCarCompany(CarCompany carCompany);
        Task<int> UpdateCarCompany(CarCompany carCompany);
        Task<int> DeleteCarCompany(CarCompany carCompany);
        #endregion

        #region Car Model
        Task<CarModelList> GetCarModelList();
        Task<int> InsertCarModel(CarModel carModel);
        Task<int> UpdateCarModel(CarModel carModel);
        Task<int> DeleteCarModel(CarModel carModel);
        #endregion

        #region Pricing
        Task<PricingList> GetPricingList();
        Task<int> InsertPricing(Pricing pricing);
        Task<int> UpdatePricing(Pricing pricing);
        Task<int> DeletePricing(Pricing pricing);
        #endregion

        #region Order
        Task<OrderList> GetOrderList();
        Task<int> InsertOrder(Order order);
        Task<int> UpdateOrder(Order order);
        Task<int> DeleteOrder(Order order);
        #endregion
    }

}
