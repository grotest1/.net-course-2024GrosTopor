using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public static class BankService
    {
        public static void CalculateSalary(int arrive, int expenses, Employee[] employees)
        {
            int empCount = employees.Count();
            if (empCount > 0)
            {
                int salaryForEmployee = (arrive - expenses) / empCount;

                foreach (Employee employee in employees)
                {
                    employee.Salary = salaryForEmployee;
                }
            }
        }

        public static Employee SetClientAsEmployee(Client client)
        {
            Employee emp = new Employee();
            emp.Name = client.Name;
            
            return emp;
        }
    }
}