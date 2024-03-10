using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiServiceNM
{
    public class ApiService : IApiService
    {
        #region Car Company
        public async Task<CarCompanyList> GetCarCompanyList()
        {
            HttpClient client = new HttpClient();

            CarCompanyList carCompanyList = null;
            try
            {
                string URI = "http://localhost:5139/api/CarCompany/SelectAllCarCompanies";
                carCompanyList = await client.GetFromJsonAsync<CarCompanyList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carCompanyList;
        }

        public async Task<int> InsertCarCompany(CarCompany carCompany)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/CarCompany/InsertCarCompany";
            var x = await httpClient.PostAsJsonAsync<CarCompany>(URI, carCompany);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCarCompany(CarCompany carCompany)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/CarCompany/UpdateCarCompany";
            HttpResponseMessage response = await client.PutAsJsonAsync<CarCompany>(URI, carCompany);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCarCompany(CarCompany carCompany)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/CarCompany/DeleteCarCompany/" + carCompany.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Car Model
        public async Task<CarModelList> GetCarModelList()
        {
            HttpClient client = new HttpClient();

            CarModelList carModelList = null;
            try
            {
                string URI = "http://localhost:5139/api/CarModel/SelectAllCarModels";
                carModelList = await client.GetFromJsonAsync<CarModelList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carModelList;
        }

        public async Task<int> InsertCarModel(CarModel carModel)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/CarModel/InsertCarModel";
            var x = await httpClient.PostAsJsonAsync<CarModel>(URI, carModel);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCarModel(CarModel carModel)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/CarModel/UpdateCarModel";
            HttpResponseMessage response = await client.PutAsJsonAsync<CarModel>(URI, carModel);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCarModel(CarModel carModel)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/CarModel/DeleteCarModel/" + carModel.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Customer
        public async Task<CustomerList> GetCustomerList()
        {
            HttpClient client = new HttpClient();

            CustomerList customerList = null;
            try
            {
                string URI = "http://localhost:5139/api/Customer/SelectAllCustomers";
                customerList = await client.GetFromJsonAsync<CustomerList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return customerList;
        }

        public async Task<int> InsertCustomer(Customer customer)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Customer/InsertCustomer";
            var x = await httpClient.PostAsJsonAsync<Customer>(URI, customer);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Customer/UpdateCustomer";
            HttpResponseMessage response = await client.PutAsJsonAsync<Customer>(URI, customer);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCustomer(Customer customer)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Customer/DeleteCustomer/" + customer.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Employee
        public async Task<EmployeeList> GetEmployeeList()
        {
            HttpClient client = new HttpClient();

            EmployeeList employeeList = null;
            try
            {
                string URI = "http://localhost:5139/api/Employee/SelectAllEmployees";
                employeeList = await client.GetFromJsonAsync<EmployeeList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return employeeList;
        }

        public async Task<int> InsertEmployee(Employee employee)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Employee/InsertEmployee";
            var x = await httpClient.PostAsJsonAsync<Employee>(URI, employee);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Employee/UpdateEmployee";
            HttpResponseMessage response = await client.PutAsJsonAsync<Employee>(URI, employee);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteEmployee(Employee employee)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Employee/DeleteEmployee/" + employee.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Manager
        public async Task<ManagerList> GetManagerList()
        {
            HttpClient client = new HttpClient();

            ManagerList managerList = null;
            try
            {
                string URI = "http://localhost:5139/api/Manager/SelectAllManagers";
                managerList = await client.GetFromJsonAsync<ManagerList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return managerList;
        }

        public async Task<int> InsertManager(Manager manager)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Manager/InsertManager";
            var x = await httpClient.PostAsJsonAsync<Manager>(URI, manager);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateManager(Manager manager)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Manager/UpdateManager";
            HttpResponseMessage response = await client.PutAsJsonAsync<Manager>(URI, manager);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteManager(Manager manager)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Manager/DeleteManager/" + manager.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Order
        public async Task<OrderList> GetOrderList()
        {
            HttpClient client = new HttpClient();

            OrderList orderList = null;
            try
            {
                string URI = "http://localhost:5139/api/Order/SelectAllOrders";
                orderList = await client.GetFromJsonAsync<OrderList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return orderList;
        }

        public async Task<int> InsertOrder(Order order)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Order/InsertOrder";
            var x = await httpClient.PostAsJsonAsync<Order>(URI, order);

            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateOrder(Order order)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Order/UpdateOrder";
            HttpResponseMessage response = await client.PutAsJsonAsync<Order>(URI, order);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteOrder(Order order)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Order/DeleteOrder/" + order.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Person
        public async Task<PersonList> GetPersonList()
        {
            HttpClient client = new HttpClient();

            PersonList personList = null;
            try
            {
                string URI = "http://localhost:5139/api/Person/SelectAllPersons";
                personList = await client.GetFromJsonAsync<PersonList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return personList;
        }

        public async Task<int> InsertPerson(Person person)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Person/InsertPerson";
            var x = await httpClient.PostAsJsonAsync<Person>(URI, person);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Person/UpdatePerson";
            HttpResponseMessage response = await client.PutAsJsonAsync<Person>(URI, person);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeletePerson(Person person)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Person/DeletePerson/" + person.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Pricing
        public async Task<PricingList> GetPricingList()
        {
            HttpClient client = new HttpClient();

            PricingList priceList = null;
            try
            {
                string URI = "http://localhost:5139/api/Pricing/SelectAllPricings";
                priceList = await client.GetFromJsonAsync<PricingList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return priceList;
        }

        public async Task<int> InsertPricing(Pricing price)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Pricing/InsertPricing";
            var x = await httpClient.PostAsJsonAsync<Pricing>(URI, price);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdatePricing(Pricing price)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Pricing/UpdatePricing";
            HttpResponseMessage response = await client.PutAsJsonAsync<Pricing>(URI, price);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeletePricing(Pricing price)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Pricing/DeletePricing/" + price.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Role
        public async Task<RoleList> GetRoleList()
        {
            HttpClient client = new HttpClient();

            RoleList roleList = null;
            try
            {
                string URI = "http://localhost:5139/api/Role/SelectAllRoles";
                roleList = await client.GetFromJsonAsync<RoleList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return roleList;
        }

        public async Task<int> InsertRole(Role role)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Role/InsertRole";
            var x = await httpClient.PostAsJsonAsync<Role>(URI, role);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateRole(Role role)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Role/UpdateRole";
            HttpResponseMessage response = await client.PutAsJsonAsync<Role>(URI, role);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteRole(Role role)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Role/DeleteRole/" + role.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Specialization
        public async Task<SpecializationList> GetSpecializationList()
        {
            HttpClient client = new HttpClient();

            SpecializationList specializationList = null;
            try
            {
                string URI = "http://localhost:5139/api/Specialization/SelectAllSpecializations";
                specializationList = await client.GetFromJsonAsync<SpecializationList>(URI);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return specializationList;
        }

        public async Task<int> InsertSpecialization(Specialization specialization)
        {
            HttpClient httpClient = new HttpClient();

            String URI = "http://localhost:5139/api/Specialization/InsertSpecialization";
            var x = await httpClient.PostAsJsonAsync<Specialization>(URI, specialization);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateSpecialization(Specialization specialization)
        {
            HttpClient client = new HttpClient();

            string URI = "http://localhost:5139/api/Specialization/UpdateSpecialization";
            HttpResponseMessage response = await client.PutAsJsonAsync<Specialization>(URI, specialization);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteSpecialization(Specialization specialization)
        {
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync("http://localhost:5139/api/Specialization/DeleteSpecialization/" + specialization.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion
    }
}

#region אופציה נוספת לעשות מחיקה
//using (var cli = new HttpClient())
//{
//    string uri = "http://localhost:5139/api/CarCompany/DeleteCarCompany/";
//    uri += company.Id.ToString();

//    string strJson = JsonSerializer.Serialize<CarCompany>(company);

//    var request = new HttpRequestMessage();
//    request.Method = HttpMethod.Delete;
//    request.RequestUri = new Uri(uri);
//    request.Content = new StringContent(strJson, Encoding.UTF8, "application/json");

//    var apiResponse = await cli.SendAsync(request);
//    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
//    {
//        num = 1;
//    }
//}
#endregion