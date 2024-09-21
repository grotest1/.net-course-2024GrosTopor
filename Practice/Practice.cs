using BankSystem.App.Services;
using BankSystem.Domain.Models;



internal class Program
{

    private static void Main(string[] args)
    {

        Employee employee_1 = new Employee("Ионел") {Birthday = new DateOnly(1999, 9, 9)};
        UpdateEmployeeContract(employee_1);


        Currency rub = new Currency();
        UpdateCurrency(ref rub);


        Client client = new Client("Василий");
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
    }
}