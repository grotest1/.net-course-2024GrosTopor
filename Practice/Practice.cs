using BankSystem.App.Services;
using BankSystem.Domain.Models;



internal class Program
{

    private static void Main(string[] args)
    {

        Employee employee = new Employee() {Name = "Ион", Birthday = new DateOnly(1999, 9, 9)};
        UpdateEmployeeContract(employee);

        Currency rub = new Currency();
        UpdateCurrency(ref rub);



        //Client client = new Client();
        //BankService.SetClientAsEmployee(client);




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