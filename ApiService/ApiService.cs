using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiServiceNM
{
    public class ApiService : IApiService
    {
        public static string Uri = "https://fdf1-2a0d-6fc2-43b0-ef00-184c-4f70-63d1-837f.ngrok-free.app";
        #region Person
        public async Task<PersonList> GetPersonList()
        {
            
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת האנשים המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            PersonList personList = null;
            try
            {
                string URI = $"{Uri}/api/Person/SelectAllPersons";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף איש חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Person/InsertPerson";
            var x = await httpClient.PostAsJsonAsync<Person>(URI, person);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי איש
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Person/UpdatePerson";
            HttpResponseMessage response = await client.PutAsJsonAsync<Person>(URI, person);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeletePerson(Person person)
        {
            //הפעולה מוחקת רשומת איש מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Person/DeletePerson/"
                    + person.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת העובדים המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            EmployeeList employeeList = null;
            try
            {
                string URI = $"{Uri}/api/Employee/SelectAllEmployees";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף עובד חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Employee/InsertEmployee";
            var x = await httpClient.PostAsJsonAsync<Employee>(URI, employee);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי עובד
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Employee/UpdateEmployee";
            HttpResponseMessage response = await client.PutAsJsonAsync<Employee>(URI, employee);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteEmployee(Employee employee)
        {
            //הפעולה מוחקת רשומת עובד מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Employee/DeleteEmployee/"
                    + employee.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת המנהלים המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            ManagerList managerList = null;
            try
            {
                string URI = $"{Uri}/api/Manager/SelectAllManagers";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף מנהל חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Manager/InsertManager";
            var x = await httpClient.PostAsJsonAsync<Manager>(URI, manager);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateManager(Manager manager)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי מנהל
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Manager/UpdateManager";
            HttpResponseMessage response = await client.PutAsJsonAsync<Manager>(URI, manager);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteManager(Manager manager)
        {
            //הפעולה מוחקת רשומת מנהל מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Manager/DeleteManager/"
                    + manager.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת הלקוחות המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            CustomerList customerList = null;
            try
            {
                string URI = $"{Uri}/api/Customer/SelectAllCustomers";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף לקוח חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Customer/InsertCustomer";
            var x = await httpClient.PostAsJsonAsync<Customer>(URI, customer);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי לקוח
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Customer/UpdateCustomer";
            HttpResponseMessage response = await client.PutAsJsonAsync<Customer>(URI, customer);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCustomer(Customer customer)
        {
            //הפעולה מוחקת רשומת לקוח מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Customer/DeleteCustomer/"
                    + customer.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת ההתמחויות המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            SpecializationList specializationList = null;
            try
            {
                string URI = $"{Uri}/api/Specialization/SelectAllSpecializations";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף התמחות חדשה
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Specialization/InsertSpecialization";
            var x = await httpClient.PostAsJsonAsync<Specialization>(URI, specialization);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateSpecialization(Specialization specialization)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי התמחות
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Specialization/UpdateSpecialization";
            HttpResponseMessage response = await client.PutAsJsonAsync<Specialization>(URI, specialization);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteSpecialization(Specialization specialization)
        {
            //הפעולה מוחקת רשומת התמחות מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Specialization/DeleteSpecialization/"
                    + specialization.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת התפקידים המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            RoleList roleList = null;
            try
            {
                string URI = $"{Uri}/api/Role/SelectAllRoles";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף תפקיד חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Role/InsertRole";
            var x = await httpClient.PostAsJsonAsync<Role>(URI, role);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateRole(Role role)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי תפקיד
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Role/UpdateRole";
            HttpResponseMessage response = await client.PutAsJsonAsync<Role>(URI, role);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteRole(Role role)
        {
            //הפעולה מוחקת רשומת תפקיד מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Role/DeleteRole/"
                    + role.Id)).IsSuccessStatusCode ? 1 : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return num;
        }
        #endregion

        #region Car Company
        public async Task<CarCompanyList> GetCarCompanyList()
        {
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת חברות הרכב המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            CarCompanyList carCompanyList = null;
            try
            {
                string URI = $"{Uri}/api/CarCompany/SelectAllCarCompanies";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף חברת רכב חדשה
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/CarCompany/InsertCarCompany";
            var x = await httpClient.PostAsJsonAsync<CarCompany>(URI, carCompany);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCarCompany(CarCompany carCompany)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי חברת רכב
            //קיימת במסד הנתונים, על ידי שליחת הנתונים החברה לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/CarCompany/UpdateCarCompany";
            HttpResponseMessage response = await client.PutAsJsonAsync<CarCompany>(URI, carCompany);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCarCompany(CarCompany carCompany)
        {
            //הפעולה מוחקת רשומת חברת רכב מסוימת בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/CarCompany/DeleteCarCompany/" +
                    carCompany.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת מודלי הרכב המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            CarModelList carModelList = null;
            try
            {
                string URI = $"{Uri}/api/CarModel/SelectAllCarModels";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף מודל רכב חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/CarModel/InsertCarModel";
            var x = await httpClient.PostAsJsonAsync<CarModel>(URI, carModel);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateCarModel(CarModel carModel)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי מודל רכב
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/CarModel/UpdateCarModel";
            HttpResponseMessage response = await client.PutAsJsonAsync<CarModel>(URI, carModel);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteCarModel(CarModel carModel)
        {
            //הפעולה מוחקת רשומת מודל רכב מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/CarModel/DeleteCarModel/"
                    + carModel.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת המחירים המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            PricingList priceList = null;
            try
            {
                string URI = $"{Uri}/api/Pricing/SelectAllPricings";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף מחיר חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Pricing/InsertPricing";
            var x = await httpClient.PostAsJsonAsync<Pricing>(URI, price);
            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdatePricing(Pricing price)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי מחיר
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Pricing/UpdatePricing";
            HttpResponseMessage response = await client.PutAsJsonAsync<Pricing>(URI, price);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeletePricing(Pricing price)
        {
            //הפעולה מוחקת רשומת מחיר מסוים בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Pricing/DeletePricing/"
                    + price.Id)).IsSuccessStatusCode ? 1 : 0;
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
            //הפעולה מבצעת בקשת HTTP GET לשרת API ומחזירה את רשימת ההזמנות המתקבלת מהשרת בפורמט JSON.
            HttpClient client = new HttpClient();

            OrderList orderList = null;
            try
            {
                string URI = $"{Uri}/api/Order/SelectAllOrders";
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
            //הפעולה מבצעת בקשת HTTP POST לשרת API על מנת להוסיף הזמנה חדש
            //למסד הנתונים באמצעות הצפנת הנתונים בתוך גוף הבקשה בפורמט JSON
            //מחזירה 1 במקרה של הצלחה או 0 במקרה של כישלון
            HttpClient httpClient = new HttpClient();

            String URI = $"{Uri}/api/Order/InsertOrder";
            var x = await httpClient.PostAsJsonAsync<Order>(URI, order);

            if (x.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> UpdateOrder(Order order)
        {
            //הפעולה שולחת בקשת HTTP PUT לשרת API כדי לעדכן פרטי הזמנה
            //קיימת במסד הנתונים, על ידי שליחת הנתונים לעדכון בפורמט JSON
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה או 0 אם היתה כישלון בביצוע הבקשה
            HttpClient client = new HttpClient();

            string URI = $"{Uri}/api/Order/UpdateOrder";
            HttpResponseMessage response = await client.PutAsJsonAsync<Order>(URI, order);
            if (response.IsSuccessStatusCode)
                return 1;
            else
                return 0;
        }

        public async Task<int> DeleteOrder(Order order)
        {
            //הפעולה מוחקת רשומת הזמנה מסוימת בשרת ה-API באמצעות כתובת URL ועל ידי שליחת בקשה HTTP DELETE
            //היא מחזירה 1 אם הבקשה הושלמה בהצלחה, ואם לא, היא מחזירה 0
            //אם המחיקה נכשלת, היא מדפיסה את הודעת השגיאה לצורך ניפוי ומחזירה 0.
            HttpClient client = new HttpClient();
            int num = 0;
            try
            {
                return (await client.DeleteAsync($"{Uri}/api/Order/DeleteOrder/"
                    + order.Id)).IsSuccessStatusCode ? 1 : 0;
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
//    string uri = $"{URI}/api/CarCompany/DeleteCarCompany/";
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