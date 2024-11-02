using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorageEF : IStorage<Employee>, IDisposable
    {
        private readonly BankSystemDbContext db = new BankSystemDbContext();

        public async Task AddAsync(Employee employee)
        {
            await Task.Run(() =>
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            });
        }

        public async Task UpdateAsync(Employee employee)
        {
            await Task.Run(() =>
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
            });
        }

        public async Task DeleteAsync(Employee employee)
        {
            await Task.Run(() =>
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            });
        }

        public async Task<List<Employee>> GetAsync(Func<Employee, bool> filter)
        {
            return await Task.Run(() => db.Employees.Where(filter).ToList());
        }

        public void Dispose()
        {
            //db.Dispose();
        }
    }
}
