using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;



internal class Program
{

    private static void Main(string[] args)
    {    
        Stopwatch stopwatch = new Stopwatch();

        List<Client> clientsList = TestDataGenerator.GenerateClientList1000();
        Dictionary<string, Client> clientsDictionary = TestDataGenerator.GenerateClientDictionary1000();
        List<Employee> employeesList = TestDataGenerator.GenerateEmployeeList1000();


        string phone = "777122345";
        Client clientToFind = new Client { PersonalPhoneNumber =  phone};
        clientsList.Add(clientToFind);
        clientsDictionary.Add(phone, clientToFind);


        stopwatch.Start();
        var result1 = clientsList.FirstOrDefault(c => c.PersonalPhoneNumber == phone);
        stopwatch.Stop();
        long duractionListFindInMS = stopwatch.ElapsedMilliseconds;


        stopwatch.Restart();
        var result2 = clientsDictionary[phone];
        stopwatch.Stop();
        long duractionDictionaryFindInMS = stopwatch.ElapsedMilliseconds;


        int ageVal = 30;
        DateTime dateTimeNow = DateTime.Now;
        DateOnly dateVal = new DateOnly(dateTimeNow.Year - ageVal, dateTimeNow.Month, dateTimeNow.Day);

        stopwatch.Restart();
        var result3 = clientsList.Where(c => c.Birthday < dateVal).ToList();
        stopwatch.Stop();
        long duractionClientsAgeFindInMS = stopwatch.ElapsedMilliseconds;


        stopwatch.Restart();
        var result4 = employeesList.Where(e => e.Salary > 0).OrderBy(e => e.Salary).FirstOrDefault();
        stopwatch.Stop();
        long duractionEmployeeFindInMS = stopwatch.ElapsedMilliseconds;

    }
}