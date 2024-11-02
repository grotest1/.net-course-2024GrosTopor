using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using AutoMapper;
using BankSystem.App.Dto;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private IStorage<Employee> _employeeStorage;
        private readonly IMapper _mapper;

        public EmployeeService(IStorage<Employee> employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }


        public EmployeeService(IStorage<Employee> employeeStorage, IMapper mapper) : this(employeeStorage)
        {
            _mapper = mapper;
        }

        public async Task<Guid> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            Employee employeet = _mapper.Map<Employee>(employeeDto);
            await Task.Run(() => AddEmployee(employeet));
            return employeet.Id;
        }

        public async Task UpdateEmployeetAsync(EmployeeDto EmployeetDto)
        {
            Employee Employeet = _mapper.Map<Employee>(EmployeetDto);
            await Task.Run(() => UpdateEmployee(Employeet));
        }

        public async Task DeleteEmployeetAsync(Guid id)
        {
            await Task.Run(() =>
            {
                Employee? employeet = GetEmployee(id);
                DeleteEmployee(employeet);
            });
        }

        public EmployeeDto? GetEmployeeDto(Guid employeeId)
        {
            Employee? employee = GetEmployee(employeeId);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public List<EmployeeDto> GetEmployeesDto()
        {
            List<Employee> employees = GetEmployees(c => true);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }


        public void AddEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
                throw new EmptyRequiredDataException("Name");
            else if (employee.Age < 18)
                throw new UnderAgeException(employee.Age);
            else if (string.IsNullOrEmpty(employee.Passport))
                throw new EmptyRequiredDataException("Passport");

            _employeeStorage.AddAsync(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (GetEmployee(employee.Id) == null)
                throw new MissingDataException("Сотрудник не найден");

            _employeeStorage.UpdateAsync(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _employeeStorage.DeleteAsync(employee);
        }

        public Employee? GetEmployee(Guid employeeId)
        {
            return _employeeStorage.GetAsync(c => c.Id == employeeId).Result.FirstOrDefault();
        }

        public List<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return _employeeStorage.GetAsync(predicate).Result;
        }
    }
}