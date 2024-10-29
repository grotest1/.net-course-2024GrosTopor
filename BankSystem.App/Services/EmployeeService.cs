using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private IStorage<Employee> _employeeStorage;

        public EmployeeService(IStorage<Employee> employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
                throw new EmptyRequiredDataException("Name");
            else if (employee.Age < 18)
                throw new UnderAgeException(employee.Age);
            else if (string.IsNullOrEmpty(employee.Passport))
                throw new EmptyRequiredDataException("Passport");

            await Task.Run(() => _employeeStorage.Add(employee));
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (GetEmployeeAsync(employee.Id).Result == null)
                throw new MissingDataException("Сотрудник не найден");

            await Task.Run(() => _employeeStorage.Update(employee));
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await Task.Run(() => _employeeStorage.Delete(employee));
        }

        public async Task<Employee?> GetEmployeeAsync(Guid employeeId)
        {
            return await Task.Run(() => _employeeStorage.Get(c => c.Id == employeeId).FirstOrDefault());
        }

        public async Task<List<Employee>> GetEmployeesAsync(Func<Employee, bool> predicate)
        {
            return await Task.Run(() => _employeeStorage.Get(predicate));
        }
    }
}
