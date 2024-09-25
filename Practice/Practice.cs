using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;



internal class Program
{

    private static void Main(string[] args)
    {    
        List<Client> clientsList = TestDataGenerator.GenerateClientList1000();
        List<Client> clientsListBogus = TestDataGenerator.GenerateClientList1000Bogus();
        Dictionary<string, Client> clientsDictionary = TestDataGenerator.GenerateClientDictionary1000();
        List<Employee> employeesList = TestDataGenerator.GenerateEmployeeList1000();


        string phone = "777122345";
        Client clientToFind = new Client { PersonalPhoneNumber =  phone};
        clientsList.Add(clientToFind);
        clientsDictionary.Add(phone, clientToFind);


        Stopwatch stopwatch = Stopwatch.StartNew();
        Client? clientByPhyone_List = clientsList.FirstOrDefault(c => c.PersonalPhoneNumber == phone);
        stopwatch.Stop();
        Console.WriteLine("Найти клиента в списке - Первый - " + stopwatch.ElapsedTicks);


        stopwatch.Restart();
        Client clientByPhyone_Dictionary = clientsDictionary[phone];
        stopwatch.Stop();
        Console.WriteLine("Найти клиента в словаре - По ключу - " + stopwatch.ElapsedTicks);


        int ageVal = 30;
        DateTime dateTimeNow = DateTime.Now;
        DateOnly dateVal = new DateOnly(dateTimeNow.Year - ageVal, dateTimeNow.Month, dateTimeNow.Day);
        stopwatch.Restart();
        List<Client> clientsFilterByBirthday = clientsList.Where(c => c.Birthday < dateVal).ToList();
        stopwatch.Stop();
        Console.WriteLine("Найти клиента в словаре Отбор - " + stopwatch.ElapsedTicks);


        stopwatch.Restart();
        Employee? employeeMinSalary = employeesList.OrderBy(e => e.Salary).FirstOrDefault();
        stopwatch.Stop();
        Console.WriteLine("Найти клиента в словаре Отбор, Сортировка и Первый - " + stopwatch.ElapsedTicks);


        stopwatch.Restart();
        KeyValuePair<string, Client> lastKeyValPair = clientsDictionary.LastOrDefault();
        stopwatch.Stop();
        Console.WriteLine("Словать - Последний - " + stopwatch.ElapsedTicks);


        stopwatch.Restart();
        var result6 = clientsDictionary[lastKeyValPair.Key];
        stopwatch.Stop();
        Console.WriteLine("Словать - По ключу - " + stopwatch.ElapsedTicks);
    }
}