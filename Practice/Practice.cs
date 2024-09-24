using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;



internal class Program
{

    private static void Main(string[] args)
    {

        #region RefAndValTypes

        Employee employee_1 = new Employee() {Name = "Ионел", Birthday = new DateOnly(1999, 9, 9)};
        UpdateEmployeeContract(employee_1);


        Currency rub = new Currency();
        UpdateCurrency(ref rub);


        Client client = new Client();
        client.Name = "Иннокентий";
        var employee_2 = BankService.SetClientAsEmployee(client);


        Employee[] employees = { employee_1, employee_2};
        BankService.CalculateSalary(90_000, 14_000, employees);


        void UpdateEmployeeContract(Employee employee)
        {
            employee.Contract = $"Contract for {employee.Name} ...";
        }

        void UpdateCurrency(ref Currency currency)
        {
            currency.Name = "Rub";
            currency.Code = 456;
        }

        #endregion

    
        List<Client> clientsList = TestDataGenerator.GenerateClientList1000();
        Dictionary<string, Client> clientsDictionary = TestDataGenerator.GenerateClientDictionary1000();
        List<Employee> employeesList = TestDataGenerator.GenerateEmployeeList1000();


        Stopwatch stopwatch = new Stopwatch();

    
    
    }
}