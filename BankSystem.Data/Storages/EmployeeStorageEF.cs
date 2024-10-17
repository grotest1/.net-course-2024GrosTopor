using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorageEF : IStorage<Employee>
    {
        private readonly BankSystemDbContext db = new BankSystemDbContext();

        public void Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Update(Employee employee)
        {
            Employee? findEmployee = db.Employees.FirstOrDefault(c => c.Id == employee.Id);
            if (findEmployee != null)
            {
                findEmployee.Age = employee.Age;
                findEmployee.Name = employee.Name;
                findEmployee.Surname = employee.Surname;
                findEmployee.Passport = employee.Passport;
                findEmployee.PersonalPhoneNumber = employee.PersonalPhoneNumber;
                findEmployee.Passport = employee.Passport;
                findEmployee.Contract = employee.Contract;
                
                db.SaveChanges();
            }
        }

        public void Delete(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public List<Employee> Get(Func<Employee, bool> filter)
        {
            return db.Employees.Where(filter).ToList();
        }
    }
}
